using UnityEngine;

public abstract class BaseWeaponProjectileStrategy : ScriptableObject
{
    protected WeaponProjectile _parentProjectile;

    public virtual void FixedUpdateHook() { }

    public virtual void Initialize(WeaponProjectile parentProjectile) => _parentProjectile = parentProjectile;
}
