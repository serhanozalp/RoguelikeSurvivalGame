using System;

public class StatModifier : ICommand
{
    private Action _onExecute;

    public StatModifier(Action onExecute)
    {
        _onExecute = onExecute;
    }

    public virtual void Execute()
    {
        _onExecute?.Invoke();
    }
}
