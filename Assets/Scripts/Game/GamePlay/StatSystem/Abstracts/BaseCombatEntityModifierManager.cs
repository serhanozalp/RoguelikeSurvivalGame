using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseCombatEntityModifierManager : BaseEntityModifierManager
{
    protected BaseStatsData _statsData;
    protected BaseStatsData _runtimeStatsData;

    public BaseCombatEntityModifierManager(string statsDataPath) : base()
    {
        _statsData = Resources.Load(statsDataPath) as BaseStatsData;
        _runtimeStatsData = ScriptableObject.Instantiate(_statsData);
    }

    public override void AddModifier(EntityModifier modifier)
    {
        if (typeof(BaseFloatStatModifier).IsAssignableFrom(modifier.GetType())) (modifier as BaseFloatStatModifier).FloatStatModified += OnFloatStatModified;
        base.AddModifier(modifier);
    }

    public override void RemoveModifier(EntityModifier modifier)
    {
        base.RemoveModifier(modifier);
        if (typeof(BaseFloatStatModifier).IsAssignableFrom(modifier.GetType())) (modifier as BaseFloatStatModifier).FloatStatModified -= OnFloatStatModified;
    }

    private void OnFloatStatModified(Type floatStatModifierType) => RecalculateFloatStat(floatStatModifierType);

    private void RecalculateFloatStat(Type floatStatModifierType)
    {
        if (_statsData.TryGetStatByModifierType<float>(floatStatModifierType, out Stat<float> stat) && _runtimeStatsData.TryGetStatByModifierType<float>(floatStatModifierType, out Stat<float> runtimeStat))
        {
            runtimeStat.Value = stat.Value;
            List<BaseFloatStatModifier> filteredList = _modifierList.Where(t => Type.Equals(t.GetType(), floatStatModifierType)).Cast<BaseFloatStatModifier>().ToList();
            filteredList.Sort();
            foreach (BaseFloatStatModifier modifier in filteredList)
            {
                switch (modifier.StatCalculationType)
                {
                    case StatCalculationType.Additive:
                        runtimeStat.Value += modifier.Value;
                        break;
                    case StatCalculationType.Multiplicative:
                        runtimeStat.Value *= modifier.Value;
                        break;
                }
            }
            Debug.Log($"{runtimeStat.Name} : {runtimeStat.Value}");
        }
    }
}
