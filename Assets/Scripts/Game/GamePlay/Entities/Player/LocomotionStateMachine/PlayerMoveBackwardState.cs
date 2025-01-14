public class PlayerMoveBackwardState : BaseLocomotionState
{
    public PlayerMoveBackwardState(BaseStateMachine parentStateMachine) : base(parentStateMachine)
    {
        _moveSpeed = GameConstants.Player.Movement.MOVE_BACKWARD_SPEED;
    }
}
