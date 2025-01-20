using System;
using UnityEngine;

public class FloatStatModifierGameObject : MonoBehaviour, IPickup
{
    [SerializeField]
    private float _value;

    [SerializeField]
    private CalculationType _statCalculationType;

    [SerializeField]
    private float _timerInitialValue;

    [SerializeField, TypeFilter(typeof(BaseFloatModifier))]
    private SerializableType _floatStatModifierType;

    public void Pickup(IEntity entity)
    {
        entity.EntityModifierManager.AddModifier((EntityModifier)Activator.CreateInstance(_floatStatModifierType.Type, new object[] {_value, _statCalculationType, null, null, _timerInitialValue}));
        Destroy(this.gameObject);
    }
}
