using System;
using System.Collections.Generic;
using System.Linq;
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
        RecalculateFloatStat(floatStatModifierType);
    }

    private void RecalculateFloatStat(Type floatStatModifierType)
    {
        if (_statsData.TryGetStatByModifierType<float>(floatStatModifierType, out Stat<float> stat) && _runtimeStatsData.TryGetStatByModifierType<float>(floatStatModifierType, out Stat<float> runtimeStat))
        {
            runtimeStat.Value = stat.Value;
            List<BaseFloatStatModifier> filteredList = _statModifierList.Where(t => Type.Equals(t.GetType(), floatStatModifierType)).Cast<BaseFloatStatModifier>().ToList();
            filteredList.Sort();
            foreach (BaseFloatStatModifier modifier in filteredList)
            {
                switch(modifier.StatCalculationType)
                {
                    case StatCalculationType.Additive:
                        runtimeStat.Value += modifier.Value;
                        break;
                    case StatCalculationType.Multiplicative:
                        runtimeStat.Value *= modifier.Value;
                        break;
                }
            }
        }
    }

    ~BaseStatsManager()
    {
        BaseFloatStatModifier.Executed -= OnFloatStatModifierExecuted;
    }
}