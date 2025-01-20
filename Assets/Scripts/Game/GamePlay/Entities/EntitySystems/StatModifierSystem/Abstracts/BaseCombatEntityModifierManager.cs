using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;
using UnityEngine;

public abstract class BaseCombatEntityModifierManager : BaseEntityModifierManager
{
    protected BaseStatsData _statsData;
    protected BaseStatsData _runtimeStatsData;

    public BaseCombatEntityModifierManager([Inject(Id = "Base")] BaseStatsData statsData, [Inject(Id = "Runtime")] BaseStatsData runtimeStatsData) : base()
    {
        _statsData = statsData;
        _runtimeStatsData = runtimeStatsData;
        BaseFloatModifier.RecalculateStat += OnRecalculateStat;
    }

    private void OnRecalculateStat(Type floatModifierType)
    {
        List<Stat<float>> stats = _statsData.GetStatsByModifierType<float>(floatModifierType);
        List<Stat<float>> runtimeStats = _runtimeStatsData.GetStatsByModifierType<float>(floatModifierType);

        for (int i = 0; i < runtimeStats.Count; i++)
        {
            runtimeStats[i].Value = stats[i].Value;
            List<BaseFloatModifier> filteredList = _modifierList.Where(t => Type.Equals(t.GetType(), floatModifierType)).Cast<BaseFloatModifier>().ToList();
            filteredList.Sort();
            foreach (BaseFloatModifier modifier in filteredList)
            {
                switch (modifier.CalculationType)
                {
                    case CalculationType.Additive:
                        runtimeStats[i].Value += modifier.Value;
                        break;
                    case CalculationType.Multiplicative:
                        runtimeStats[i].Value *= modifier.Value;
                        break;
                }
                Debug.Log($"{runtimeStats[i].Name}: {runtimeStats[i].Value}");
            }
        }
    }

    ~BaseCombatEntityModifierManager()
    {
        BaseFloatModifier.RecalculateStat -= OnRecalculateStat;
    }
}
