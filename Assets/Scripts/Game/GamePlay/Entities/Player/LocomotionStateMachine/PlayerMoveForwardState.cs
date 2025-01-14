public class PlayerMoveForwardState : BaseLocomotionState
{
    public PlayerMoveForwardState(BaseStateMachine parentStateMachine) : base(parentStateMachine)
    {
        _moveSpeed = GameConstants.Player.Movement.MOVE_FORWARD_SPEED;
    }
}
