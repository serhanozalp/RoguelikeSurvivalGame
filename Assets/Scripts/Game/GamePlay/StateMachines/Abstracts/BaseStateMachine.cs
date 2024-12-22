public abstract class BaseStateMachine<T> : BaseState where T : class
{
    protected BaseState _currentState;
    protected T _contextData;

    protected BaseStateMachine(BaseState parentState, T contextData) : base(parentState)
    {
        _contextData = contextData;
    }

    protected void ChangeState(BaseState baseState)
    {
        if (_currentState == baseState) return;
        _currentState?.Exit();
        _currentState = baseState;
        _currentState.Enter();
    }

    public override void OnUpdate()
    {
        StateTransitionLogic();
        _currentState?.OnUpdate();
    }

    protected abstract void StateTransitionLogic();
}
