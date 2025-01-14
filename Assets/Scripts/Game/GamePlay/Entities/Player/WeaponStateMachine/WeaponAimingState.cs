using UnityEngine;

public class WeaponAimingState : BaseWeaponState
{
    private bool _isAiming;

    public WeaponAimingState(BaseStateMachine parentStateMachine) : base(parentStateMachine)
    {
    }

    public override async void Enter()
    {
        using (new SequenceScope(
          () => _parentStateMachine.IsStateTransitionLocked = true,
          () => _parentStateMachine.IsStateTransitionLocked = false))
        {
            await _playerAnimation.PlayWeaponAimAnimation();
            SetAimTargetHeight();
            _isAiming = true;
        }
    }

    public override void OnUpdate()
    {
        if (!_playerInput.IsLeftMouseButtonHeldDown) _parentStateMachine.ChangeState<WeaponIdleState>();
        else if (_playerInput.IsLeftMouseButtonHeldDown && _isAiming) _playerWeaponManager.CurrentWeapon.Fire();
    }

    public override void Exit() => _isAiming = false;

    private void SetAimTargetHeight() => _playerWeaponManager.AimIKTarget.position = new Vector3(_playerWeaponManager.AimIKTarget.position.x, _playerWeaponManager.CurrentWeapon.WeaponData.AimIKTransform.position.y, _playerWeaponManager.AimIKTarget.position.z);
}
