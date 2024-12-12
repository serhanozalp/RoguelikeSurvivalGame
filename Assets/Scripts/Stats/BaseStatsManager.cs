using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStatsManager
{
    protected BaseStatsData _statsData;
    protected BaseStatsData _runtimeStatsData;

    protected readonly List<StatModifier> _statModifierList;

    public BaseStatsManager(string statsDataPath)
    {
        _statsData =  Resources.Load(statsDataPath) as BaseStatsData;
        ResetRuntimeStatsData();
        _statModifierList = new List<StatModifier>();
    }

    public void AddStatModifier(StatModifier statModifier)
    {
        _statModifierList.Add(statModifier);
    }

    public BaseStatsData GetRuntimeStatsData(bool update)
    {
        if (update)
        {
            ResetRuntimeStatsData();
            UpdateRuntimeStatsData();
        }
        return _runtimeStatsData;
    }

    protected void UpdateRuntimeStatsData()
    {
        foreach (StatModifier statModifier in _statModifierList)
        {
            statModifier.Execute();
        }
    }

    protected void ResetRuntimeStatsData()
    {
        _runtimeStatsData = ScriptableObject.Instantiate(_statsData);
    }
}
