public abstract class BaseState<T> where T : class
{
    protected BaseState<T> _parentState;
    protected T _contextData;

    public BaseState(BaseState<T> parentState, T contextData)
    {
        _contextData = contextData;
        if (parentState == null)
        {
            _parentState = this;
            Enter();
        }
        else _parentState = parentState;
    }

    public virtual void Enter()
    {
        HandleAnimations();
    }

    public abstract void OnUpdate();
    public abstract void OnLateUpdate();
    public abstract void Exit();

    protected virtual void HandleAnimations() { }
}