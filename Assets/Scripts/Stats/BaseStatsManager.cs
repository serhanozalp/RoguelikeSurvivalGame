using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStatsManager
{
    protected BaseStatsData _statsData;
    protected BaseStatsData _runtimeStatsData;

    protected readonly List<BaseStatModifier> _statModifierList;

    public BaseStatsManager(string statsDataPath)
    {
        _statsData =  Resources.Load(statsDataPath) as BaseStatsData;
        _statModifierList = new List<BaseStatModifier>();
        BaseFloatStatModifier.Executed += OnFloatStatModifierExecuted;
    }

    public void AddStatModifier(BaseStatModifier statModifier)
    {
        _statModifierList.Add(statModifier);
        statModifier.Execute();
    }

    private void OnFloatStatModifierExecuted(Type floatStatModifierType)
    {
        if(_statsData.TryGetStatByModifierType<float>(floatStatModifierType, out Stat<float> stat))
        {
            Debug.Log(stat.Value);
        }
    }

    ~BaseStatsManager()
    {
        BaseFloatStatModifier.Executed -= OnFloatStatModifierExecuted;
    }
}
