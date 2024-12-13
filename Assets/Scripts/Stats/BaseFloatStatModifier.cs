using System;

public enum StatCalculationType
{
    Additive,
    Multiplicative,
}

public abstract class BaseFloatStatModifier : BaseStatModifier
{
    private StatCalculationType _statCalculationType;
    private float _value;

    public static Action<Type> Executed;


    public BaseFloatStatModifier(Action onExecute, float value, StatCalculationType statCalculationType) : base(onExecute)
    {
        _value = value;
        _statCalculationType = statCalculationType;
    }

    public override void Execute()
    {
        base.Execute();
        Executed?.Invoke(this.GetType());
    }
}
