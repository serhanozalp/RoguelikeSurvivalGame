public abstract class BaseState
{
    protected BaseState _parentState;

    public BaseState(BaseState parentState)
    {
        if (parentState == null)
        {
            _parentState = this;
            Enter();
        }
        else _parentState = parentState;
    }

    public virtual void Enter() { }

    public virtual void OnUpdate() { }

    public virtual  void Exit() { }
}