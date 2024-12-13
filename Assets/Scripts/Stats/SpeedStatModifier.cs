using System;

public class SpeedStatModifier : BaseFloatStatModifier
{
    public SpeedStatModifier(Action onExecute, float value, StatCalculationType statCalculationType) : base(onExecute, value, statCalculationType)
    {
    }
}
