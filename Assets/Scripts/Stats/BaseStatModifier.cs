using System;

public abstract class BaseStatModifier : ICommand
{
    private Action _onExecute;

    public BaseStatModifier(Action onExecute)
    {
        _onExecute = onExecute;
    }

    public virtual void Execute()
    {
        _onExecute?.Invoke();
    }
}
