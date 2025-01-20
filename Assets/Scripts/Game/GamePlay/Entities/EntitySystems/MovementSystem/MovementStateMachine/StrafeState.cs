public class StrafeState : BaseMovementState
{
    public StrafeState(BaseStateMachine parentStateMachine) : base(parentStateMachine)
    {
    }

    public override void Enter()
    {
        _entityMovement.MoveSpeed = _runtimeStatsData.StrafeSpeed.Value;
    }
}
