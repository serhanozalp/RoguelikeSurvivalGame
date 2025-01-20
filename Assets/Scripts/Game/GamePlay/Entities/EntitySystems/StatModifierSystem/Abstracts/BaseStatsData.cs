using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

[Serializable]
public class Stat<T>
{
    public string Name;
    public T Value;

    [SerializeField, TypeFilter(typeof(EntityModifier))]
    public SerializableType ModifierType;
}

public abstract class BaseStatsData : ScriptableObject
{
    [Header("Movement")]
    public Stat<float> ForwardSpeed;
    public Stat<float> BackwardSpeed;
    public Stat<float> StrafeSpeed;

    [Header("Defence")]
    public Stat<float> Armor;

    [Header("Attack")]
    public Stat<float> AttackRate;
    

    public List<Stat<T>> GetStatsByModifierType<T>(Type modifierType)
    {

        List<FieldInfo> fields =  this.GetType().GetFields().ToList();
        List<Stat<T>> stats = new List<Stat<T>>();
        foreach (FieldInfo field in fields)
        {
            if (!Type.Equals(field.FieldType, typeof(Stat<T>))) continue;
            Stat<T> stat = (Stat<T>)field.GetValue(this);
            if (Type.Equals(stat.ModifierType.Type, modifierType)) stats.Add(stat);
        }
        return stats;
    }
}