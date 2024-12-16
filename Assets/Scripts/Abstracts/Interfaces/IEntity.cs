public interface IEntity
{
}

public interface ICombatEntity : IEntity
{
    public BaseStatsManager CombatEntityStatsManager { get; }
}
