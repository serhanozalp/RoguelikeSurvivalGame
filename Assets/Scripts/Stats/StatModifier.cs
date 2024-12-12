using System;


public class StatModifier : ICommand
{
    public enum StatModifierType
    {
        Additive,
        Multiplicative,
        None
    }

    private Action _onExecute;

    private StatModifierType _statModifierType;

    public StatModifier(Action onExecute, StatModifierType statModifierType)
    {
        _onExecute = onExecute;
        _statModifierType = statModifierType;
    }

    public void Execute()
    {
        _onExecute.Invoke();
    }

    public void UnDo()
    {
    }
}
