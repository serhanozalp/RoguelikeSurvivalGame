public class PlayerStrafeState : BaseLocomotionState<PlayerLocomotionContextData>
{
    public PlayerStrafeState(BaseState<PlayerLocomotionContextData> parentState, PlayerLocomotionContextData contextData) : base(parentState, contextData)
    {
        _moveSpeed = 3f;
    }

    protected override void SetMovementSpeed()
    {
        _contextData.PlayerMovement.MoveSpeed = _moveSpeed;
    }
}
