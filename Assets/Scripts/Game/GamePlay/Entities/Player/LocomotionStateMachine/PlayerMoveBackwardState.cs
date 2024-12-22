public class PlayerMoveBackwardState : BaseLocomotionState
{
    public PlayerMoveBackwardState(BaseState parentState, BaseEntityMovement entitiyMovement) : base(parentState, entitiyMovement)
    {
        _moveSpeed = GameConstants.Player.MOVE_BACKWARD_SPEED;
    }
}
