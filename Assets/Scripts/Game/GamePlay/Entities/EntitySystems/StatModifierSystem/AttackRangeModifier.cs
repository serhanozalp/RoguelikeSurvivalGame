using System;

public class AttackRangeModifier : BaseFloatModifier
{
    public AttackRangeModifier(float value, CalculationType calculationType, Action onExecute = null, Action onUndo = null, float timerInitialValue = 0) : base(value, calculationType, onExecute, onUndo, timerInitialValue)
    {
    }
}
