using Zenject;

public class PlayerEntityModifierManager : BaseCombatEntityModifierManager
{
    public PlayerEntityModifierManager([Inject(Id = "Base")] BaseStatsData statsData, [Inject(Id = "Runtime")] BaseStatsData runtimeStatsData) : base(statsData, runtimeStatsData)
    {
    }
}
