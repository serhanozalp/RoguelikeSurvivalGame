using System;
using UnityEngine;
using Infrastructure.PropertyAttributes;

[Serializable]
public struct Stat<T>
{
    public string Name;
    public T Value;
    public bool IsDirty;

    [SerializeField, TypeFilter(typeof(StatModifier))]
    public SerializableType type;
}

public abstract class BaseStatsData : ScriptableObject
{   
    public Stat<float> Armor;
    public Stat<float> Speed;
}
