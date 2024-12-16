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
    public static Action<Type> FloatStatModified;

    public BaseFloatStatModifier(float value, StatCalculationType statCalculationType, Action onExecute = null, Action onUndo = null,  float timerInitialValue = 0) : base(onExecute, onUndo, timerInitialValue)
    {
        _value = value;
        _statCalculationType = statCalculationType;
    }

    public override void Execute()
    {
        base.Execute();
        FloatStatModified?.Invoke(this.GetType());
    }

    public override void Undo()
    {
        base.Undo();
        FloatStatModified?.Invoke(this.GetType());
    }

    public int CompareTo(BaseFloatStatModifier other) => _statCalculationType.CompareTo(other.StatCalculationType);
}
