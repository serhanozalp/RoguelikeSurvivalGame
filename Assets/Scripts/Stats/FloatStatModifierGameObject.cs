using Infrastructure.PropertyAttributes;
using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FloatStatModifierGameObject : MonoBehaviour, IPickup
{
    [SerializeField]
    private float _value;

    [SerializeField]
    StatCalculationType _statCalculationType;

    [SerializeField, TypeFilter(typeof(BaseFloatStatModifier))]
    private SerializableType _statModifierType;

    private BaseStatsManager _entityStatsManager;

    public void Pickup(IEntity entity)
    {
        _entityStatsManager = (entity as ICombatEntity).CombatEntityStatsManager;
        _entityStatsManager.AddStatModifier((BaseStatModifier)Activator.CreateInstance(_statModifierType.Type, null, _value, _statCalculationType));
        Destroy(this.gameObject);
    }
    
}
