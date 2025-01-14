using UnityEngine;
using RootMotion.FinalIK;
using Zenject;
using Cysharp.Threading.Tasks;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    private PlayerWeaponManager _playerWeaponManager;
    private PlayerIKFacade _playerIKFacade;

    [Inject]
    private void ZenjectConstructor(PlayerWeaponManager playerWeaponManager, PlayerIKFacade playerIKFacade)
    {
        _playerWeaponManager = playerWeaponManager;
        _playerIKFacade = playerIKFacade;
    }

    private void Update()
    {
        _playerIKFacade.UpdateHook();
    }

    public async UniTask PlayWeaponEquipAnimation()
    {
        _playerAnimator.runtimeAnimatorController = _playerWeaponManager.CurrentWeapon.WeaponData.WeaponAOC;
        _playerAnimator.SetLayerWeight(GameConstants.Player.Animation.ARMS_LAYER_INDEX, 1);
        _playerIKFacade.SetupIKForWeaponEquip();
        _playerAnimator.CrossFadeInFixedTime(GameConstants.Player.Animation.ANIMATION_STATE_NAME_WEAPON_GRAB, GameConstants.Player.Animation.ANIMATION_FADE_DURATION);
        await UniTask.WaitUntil(() => IsAnimationFinished(GameConstants.Player.Animation.ARMS_LAYER_INDEX, GameConstants.Player.Animation.ANIMATION_STATE_NAME_WEAPON_GRAB));
    }

    public async UniTask PlayWeaponIdleAnimation()
    {
        _playerAnimator.CrossFadeInFixedTime(GameConstants.Player.Animation.ANIMATION_STATE_NAME_WEAPON_IDLE, GameConstants.Player.Animation.ANIMATION_FADE_DURATION);
        BaseWeaponData weaponData = _playerWeaponManager.CurrentWeapon.WeaponData;
        await UniTask.WhenAll(
            _playerIKFacade.MoveLimbToAndFollow(FullBodyBipedChain.LeftArm, weaponData.LeftHandIKWeaponGrip, GameConstants.Player.Animation.ANIMATION_FADE_DURATION, weaponData.LeftArmIKWeaponBend),
            _playerIKFacade.UnAimWeapon(GameConstants.Player.Animation.ANIMATION_FADE_DURATION));
    }

    public async UniTask PlayWeaponAimAnimation()
    {
        _playerAnimator.CrossFadeInFixedTime(GameConstants.Player.Animation.ANIMATION_STATE_NAME_WEAPON_AIM, GameConstants.Player.Animation.ANIMATION_FADE_DURATION);
        await _playerIKFacade.AimWeapon(_playerWeaponManager.CurrentWeapon.WeaponData.AimIKTransform, GameConstants.Player.Animation.ANIMATION_FADE_DURATION);
    }

    public async UniTask PlayWeaponReloadAnimation()
    {
        _playerIKFacade.SetupIKForWeaponReload();
        _playerAnimator.CrossFadeInFixedTime(GameConstants.Player.Animation.ANIMATION_STATE_NAME_WEAPON_RELOAD, GameConstants.Player.Animation.ANIMATION_FADE_DURATION);
        await UniTask.WhenAll(
            UniTask.WaitUntil(() => IsAnimationFinished(GameConstants.Player.Animation.ARMS_LAYER_INDEX, GameConstants.Player.Animation.ANIMATION_STATE_NAME_WEAPON_RELOAD)),
            _playerIKFacade.UnAimWeapon(GameConstants.Player.Animation.ANIMATION_FADE_DURATION));
    }

    public void SetLocomotionAnimationValues(float dotRight, float dotForward)
    {
        _playerAnimator.SetFloat("DotRight", Mathf.Lerp(_playerAnimator.GetFloat("DotRight"), dotRight, GameConstants.Player.Animation.LOCOMOTION_BLENDTREE_LERPSCALE));
        _playerAnimator.SetFloat("DotForward", Mathf.Lerp(_playerAnimator.GetFloat("DotForward"), dotForward, GameConstants.Player.Animation.LOCOMOTION_BLENDTREE_LERPSCALE));
    }

    private bool IsAnimationFinished(int layerIndex, string animationName)
    {
        AnimatorStateInfo stateInfo = _playerAnimator.GetCurrentAnimatorStateInfo(GameConstants.Player.Animation.ARMS_LAYER_INDEX);
        return stateInfo.IsName(animationName) && stateInfo.normalizedTime % 1f >= 0.99f;
    }
}
