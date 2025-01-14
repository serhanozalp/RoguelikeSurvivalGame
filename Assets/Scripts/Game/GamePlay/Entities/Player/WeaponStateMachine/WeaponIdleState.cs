public class WeaponIdleState : BaseWeaponState
{
    public WeaponIdleState(BaseStateMachine parentStateMachine) : base(parentStateMachine)
    {
    }

    public override async void Enter()
    {
        using (new SequenceScope(
           () => _parentStateMachine.IsStateTransitionLocked = true,
           () => _parentStateMachine.IsStateTransitionLocked = false))
        {
            await _playerAnimation.PlayWeaponIdleAnimation();
        }
    }

    public override void OnUpdate()
    {
        if(_playerInput.IsLeftMouseButtonHeldDown) _parentStateMachine.ChangeState<WeaponAimingState>();
    }
}
