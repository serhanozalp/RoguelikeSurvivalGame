public class MoveBackwardState : BaseMovementState
{
    public MoveBackwardState(BaseStateMachine parentStateMachine) : base(parentStateMachine)
    {
    }

    public override void Enter()
    {
        _entityMovement.MoveSpeed = _runtimeStatsData.BackwardSpeed.Value;
    }
}
