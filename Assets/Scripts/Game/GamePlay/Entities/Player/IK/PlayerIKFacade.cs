using Cysharp.Threading.Tasks;
using RootMotion.FinalIK;
using UnityEngine;

public class PlayerIKFacade : HumanoidIKFacade
{
    private AimIK _weaponAimIK;
    private float _weaponAimIKTargetWeight;
    private float _weaponAimIKLerpDuration;

    public PlayerIKFacade(FullBodyBipedIK fullBodyBipedIK, AimIK weaponAimIK) : base(fullBodyBipedIK)
    {
        _weaponAimIK = weaponAimIK;
    }

    public override void UpdateHook()
    {
        base.UpdateHook();
        UpdateWeaponAimIKWeight();
    }

    private void UpdateWeaponAimIKWeight()
    {
        if (_weaponAimIK.solver.IKPositionWeight == _weaponAimIKTargetWeight) return;
        _weaponAimIK.solver.IKPositionWeight = Mathf.MoveTowards(_weaponAimIK.solver.IKPositionWeight, _weaponAimIKTargetWeight, (1f / _weaponAimIKLerpDuration) * Time.deltaTime);
    }

    public void SetupIKForWeaponEquip()
    {
        ResetChain(FullBodyBipedChain.LeftArm);
        ResetAimIK();
    }

    public void SetupIKForWeaponReload()
    {
        ResetChain(FullBodyBipedChain.LeftArm);
    }

    public async UniTask AimWeapon(Transform weaponAimIK, float duration)
    {
        _weaponAimIK.solver.transform = weaponAimIK;
        await LerpWeaponAimIKWeight(1, duration);
    }

    public async UniTask UnAimWeapon(float duration) => await LerpWeaponAimIKWeight(0, duration);

    private async UniTask LerpWeaponAimIKWeight(float targetWeight, float lerpDuration)
    {
        _weaponAimIKLerpDuration = lerpDuration;
        _weaponAimIKTargetWeight = targetWeight;
        await UniTask.WaitUntil(() => _weaponAimIK.solver.IKPositionWeight == targetWeight);
    }

    private void ResetAimIK()
    {
        _weaponAimIK.solver.transform = null;
        _weaponAimIKTargetWeight = 0;
        _weaponAimIK.solver.IKPositionWeight = 0;
    }
}
