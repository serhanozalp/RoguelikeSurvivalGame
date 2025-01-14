using Zenject;

public class PlayerModifierManager : BaseCombatEntityModifierManager
{
    public PlayerModifierManager([Inject(Id = "Base")] BaseStatsData statsData, [Inject(Id = "Runtime")] BaseStatsData runtimeStatsData) : base(statsData, runtimeStatsData)
    {
    }
}
