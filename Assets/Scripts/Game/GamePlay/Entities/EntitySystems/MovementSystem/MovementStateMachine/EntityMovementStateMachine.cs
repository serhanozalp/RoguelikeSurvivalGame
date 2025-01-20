using Zenject;

public class EntityMovementStateMachine : BaseStateMachine
{
    IdleMovementState _idleState;
    StrafeState _strafeState;
    MoveForwardState _moveForwardState;
    MoveBackwardState _moveBackwardState;

    [Inject]
    private void ZenjectConstructor(DiContainer container)
    {
        container.QueueForInject(_strafeState);
        container.QueueForInject(_moveForwardState);
        container.QueueForInject(_moveBackwardState);
    }

    public EntityMovementStateMachine(BaseStateMachine parentStateMachine = null) : base(parentStateMachine)
    {
        AddState(_idleState = new IdleMovementState(this));
        AddState(_strafeState = new StrafeState(this));
        AddState(_moveForwardState = new MoveForwardState(this));
        AddState(_moveBackwardState = new MoveBackwardState(this));
    }
}
