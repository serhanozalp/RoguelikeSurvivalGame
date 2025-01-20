using Zenject;

public abstract class BaseMovementState : BaseState
{
    protected BaseEntityMovement _entityMovement;
    protected BaseStatsData _runtimeStatsData;

    [Inject]
    protected void ZenjectConstructor(BaseEntityMovement entityMovement, [Inject(Id = "Runtime")] BaseStatsData runtimeStatsData)
    {
        _entityMovement = entityMovement;
        _runtimeStatsData = runtimeStatsData;
    }

    protected BaseMovementState(BaseStateMachine parentStateMachine) : base(parentStateMachine)
    {
    }
}
