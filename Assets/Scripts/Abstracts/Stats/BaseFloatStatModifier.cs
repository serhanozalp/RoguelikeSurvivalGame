using System;

public enum StatCalculationType
{
    Additive,
    Multiplicative,
}

public abstract class BaseFloatStatModifier : StatModifier , IComparable<BaseFloatStatModifier>
{
    private StatCalculationType _statCalculationType;
    public StatCalculationType StatCalculationType { get { return _statCalculationType; } }
    private float _value;
    public float Value { get { return _value; } }
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

    public int CompareTo(BaseFloatStatModifier other)
    {
        return _statCalculationType.CompareTo(other.StatCalculationType);
    }
}
