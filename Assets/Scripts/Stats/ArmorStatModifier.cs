using System;

public class ArmorStatModifier : BaseFloatStatModifier
{
    public ArmorStatModifier(Action onExecute, float value, StatCalculationType statCalculationType) : base(onExecute, value, statCalculationType)
    {
    }
}
