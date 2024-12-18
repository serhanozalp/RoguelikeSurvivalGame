using Infrastructure.PropertyAttributes;
using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FloatStatModifierGameObject : MonoBehaviour, IPickup
{
    [SerializeField]
    private float _value;

    [SerializeField]
    private StatCalculationType _statCalculationType;

    [SerializeField]
    private float _timerInitialValue;

    [SerializeField, TypeFilter(typeof(BaseFloatStatModifier))]
    private SerializableType _floatStatModifierType;

    public void Pickup(IEntity entity)
    {
        entity.EntityModifierManager.AddModifier((EntityModifier)Activator.CreateInstance(_floatStatModifierType.Type, new object[] {_value, _statCalculationType, null, null, _timerInitialValue}));
        Destroy(this.gameObject);
    }
}
