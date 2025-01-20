using System;

public class SpeedModifier : BaseFloatModifier
{
    public SpeedModifier(float value, CalculationType calculationType, Action onExecute = null, Action onUndo = null, float timerInitialValue = 0) : base(value, calculationType, onExecute, onUndo, timerInitialValue)
    {
    }
}