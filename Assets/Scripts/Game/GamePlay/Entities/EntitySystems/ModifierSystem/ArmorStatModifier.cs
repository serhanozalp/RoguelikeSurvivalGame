using System;

public class ArmorStatModifier : BaseFloatStatModifier
{
    public ArmorStatModifier(float value, StatCalculationType statCalculationType, Action onExecute = null, Action onUndo = null, float timerInitialValue = 0) : base(value, statCalculationType, onExecute, onUndo, timerInitialValue)
    {
    }
}