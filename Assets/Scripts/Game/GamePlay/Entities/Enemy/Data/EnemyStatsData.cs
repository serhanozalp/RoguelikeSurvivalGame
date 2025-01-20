using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatsData", menuName = "StatsData/Enemy")]
public class EnemyStatsData : BaseStatsData
{
    [Header("Range")]
    public Stat<float> AttackRange;
    public Stat<float> SensorRange;
}
