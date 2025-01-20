using System;

public enum CalculationType
{
    Additive,
    Multiplicative,
}

public abstract class BaseFloatModifier : EntityModifier , IComparable<BaseFloatModifier>
{
    private CalculationType _calculationType;
    public CalculationType CalculationType { get { return _calculationType; } }

    private float _value;
    public float Value { get { return _value; } }

    public static Action<Type> RecalculateStat;

    public BaseFloatModifier(float value, CalculationType calculationType, Action onExecute = null, Action onUndo = null,  float timerInitialValue = 0) : base(onExecute, onUndo, timerInitialValue)
    {
        _value = value;
        _calculationType = calculationType;
    }

    public override void Execute()
    {
        base.Execute();
        RecalculateStat?.Invoke(this.GetType());
    }

    public override void Undo()
    {
        base.Undo();
        RecalculateStat?.Invoke(this.GetType());
    }

    public int CompareTo(BaseFloatModifier other) => _calculationType.CompareTo(other.CalculationType);
}
