public class WeaponReloadingState : BaseWeaponState
{
    public WeaponReloadingState(BaseStateMachine parentStateMachine) : base(parentStateMachine)
    {
    }

    public override async void Enter()
    {
        using (new SequenceScope(
            () => _parentStateMachine.IsStateTransitionLocked = true,
            () => _parentStateMachine.IsStateTransitionLocked = false))
        {
            await _playerAnimation.PlayWeaponReloadAnimation();
            _playerWeaponManager.CurrentWeapon.Reload();
        }
        _parentStateMachine.ChangeState<WeaponIdleState>();
    }
}
