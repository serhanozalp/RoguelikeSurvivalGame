using System;
using UnityEngine;
using Infrastructure.PropertyAttributes;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

[Serializable]
public class Stat<T>
{
    public string Name;
    public T Value;

    [SerializeField, TypeFilter(typeof(BaseStatModifier))]
    public SerializableType StatModifierType;
}

public abstract class BaseStatsData : ScriptableObject
{   
    public Stat<float> Armor;
    public Stat<float> Speed;


    public bool TryGetStatByModifierType<T>(Type statModifierType, out Stat<T> stat)
    {
        List<FieldInfo> fields =  this.GetType().GetFields().ToList();
        stat = null;
        foreach (FieldInfo field in fields)
        {
            stat = (Stat<T>)field.GetValue(this);
            if (Type.Equals(stat.StatModifierType.Type, statModifierType)) return true;
        }
        return false;
    }
}

