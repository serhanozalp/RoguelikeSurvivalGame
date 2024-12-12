using Infrastructure.PropertyAttributes;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ArmorModifierGameObject : MonoBehaviour, IPickup
{
    [SerializeField]
    private float _value;
    [SerializeField]
    StatModifier.StatModifierType _statModifierType;

    private BaseStatsManager _entityStatsManaeger;

    public void Pickup(IEntity entity)
    {
        _entityStatsManaeger = (entity as ICombatEntity).CombatEntityStatsManager;
        _entityStatsManaeger.AddStatModifier(new StatModifier(
            () => ApplyStatModifier() ,
            _statModifierType));

        Destroy(this.gameObject);
    }

    private void ApplyStatModifier()
    {
        switch (_statModifierType)
        {
            case StatModifier.StatModifierType.Additive:
                _entityStatsManaeger.GetRuntimeStatsData(false).Armor.Value += _value;
                break;
            case StatModifier.StatModifierType.Multiplicative:
                _entityStatsManaeger.GetRuntimeStatsData(false).Armor.Value *= _value;
                break;
        }
    }
}
