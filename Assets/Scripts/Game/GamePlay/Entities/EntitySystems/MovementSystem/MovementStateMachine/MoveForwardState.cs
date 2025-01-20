public class MoveForwardState : BaseMovementState
{
    public MoveForwardState(BaseStateMachine parentStateMachine) : base(parentStateMachine)
    { 
    }

    public override void Enter()
    {
        _entityMovement.MoveSpeed = _runtimeStatsData.ForwardSpeed.Value;
    }
}
