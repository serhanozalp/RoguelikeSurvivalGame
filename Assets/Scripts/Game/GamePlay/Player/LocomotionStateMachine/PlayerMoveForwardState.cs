public class PlayerMoveForwardState : BaseLocomotionState<PlayerLocomotionContextData>
{
    public PlayerMoveForwardState(BaseState<PlayerLocomotionContextData> parentState, PlayerLocomotionContextData contextData) : base(parentState, contextData)
    {
        _moveSpeed = 5f;
    }

    protected override void SetMovementSpeed()
    {
        _contextData.PlayerMovement.MoveSpeed = _moveSpeed;
    }
}
