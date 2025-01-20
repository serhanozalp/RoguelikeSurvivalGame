using System;

public class ArmorModifier : BaseFloatModifier
{
    public ArmorModifier(float value, CalculationType calculationType, Action onExecute = null, Action onUndo = null, float timerInitialValue = 0) : base(value, calculationType, onExecute, onUndo, timerInitialValue)
    {
    }
}