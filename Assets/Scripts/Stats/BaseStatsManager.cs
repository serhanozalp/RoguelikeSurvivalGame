using System;
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
        _runtimeStatsData = ScriptableObject.Instantiate(_statsData);
        _statModifierList = new List<StatModifier>();
        BaseFloatStatModifier.Executed += OnFloatStatModifierExecuted;
    }

    public void AddStatModifier(StatModifier statModifier)
    {
        _statModifierList.Add(statModifier);
        statModifier.Execute();
    }

    private void OnFloatStatModifierExecuted(Type floatStatModifierType)
    {
        if(_runtimeStatsData.TryGetStatByModifierType<float>(floatStatModifierType, out Stat<float> stat))
        {
            Debug.Log(stat.Value);
        }
    }

    ~BaseStatsManager()
    {
        BaseFloatStatModifier.Executed -= OnFloatStatModifierExecuted;
    }
}
