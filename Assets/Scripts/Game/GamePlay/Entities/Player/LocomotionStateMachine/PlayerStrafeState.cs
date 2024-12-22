public class PlayerStrafeState : BaseLocomotionState
{
    public PlayerStrafeState(BaseState parentState, BaseEntityMovement entitiyMovement) : base(parentState, entitiyMovement)
    {
        _moveSpeed = GameConstants.Player.STRAFE_SPEED;
    }
}
