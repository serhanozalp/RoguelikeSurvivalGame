public class PlayerStrafeState : BaseLocomotionState
{
    public PlayerStrafeState(BaseStateMachine parentStateMachine) : base(parentStateMachine)
    {
        _moveSpeed = GameConstants.Player.Movement.STRAFE_SPEED;
    }
}
