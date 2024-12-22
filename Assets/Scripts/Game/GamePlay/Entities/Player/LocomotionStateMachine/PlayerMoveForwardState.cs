public class PlayerMoveForwardState : BaseLocomotionState
{
    public PlayerMoveForwardState(BaseState parentState, BaseEntityMovement entitiyMovement) : base(parentState, entitiyMovement)
    {
        _moveSpeed = GameConstants.Player.MOVE_FORWARD_SPEED;
    }
}
