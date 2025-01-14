using Cysharp.Threading.Tasks;

public abstract class BaseState
{
    protected BaseStateMachine _parentStateMachine;

    public BaseState(BaseStateMachine parentStateMachine)
    {
        _parentStateMachine = parentStateMachine ?? this as BaseStateMachine;
    }

    public virtual void Enter() { }

    public virtual void OnUpdate() { }

    public virtual  void Exit() { }

#pragma warning disable CS1998
    public virtual async UniTask EnterAsync() { }

    public virtual async UniTask ExitAsync() { }
#pragma warning restore
}