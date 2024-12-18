public abstract class BaseStateMachine<T> : BaseState<T> where T : class
{
    protected BaseState<T> _currentState;

    protected BaseStateMachine(BaseState<T> parentState, T contextData) : base(parentState, contextData)
    {
    }

    protected void ChangeState(BaseState<T> baseState)
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
