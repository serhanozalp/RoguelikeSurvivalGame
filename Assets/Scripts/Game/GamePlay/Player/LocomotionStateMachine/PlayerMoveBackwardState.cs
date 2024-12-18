public class PlayerMoveBackwardState : BaseLocomotionState<PlayerLocomotionContextData>
{
    public PlayerMoveBackwardState(BaseState<PlayerLocomotionContextData> parentState, PlayerLocomotionContextData contextData) : base(parentState, contextData)
    {
        _moveSpeed = 2f;
    }

    protected override void SetMovementSpeed()
    {
        _contextData.PlayerMovement.MoveSpeed = _moveSpeed;
    }
}
