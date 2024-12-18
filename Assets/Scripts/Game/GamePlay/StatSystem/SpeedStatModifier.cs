using System;

public class SpeedStatModifier : BaseFloatStatModifier
{
    public SpeedStatModifier(float value, StatCalculationType statCalculationType, Action onExecute = null, Action onUndo = null, float timerInitialValue = 0) : base(value, statCalculationType, onExecute, onUndo, timerInitialValue)
    {
    }
}