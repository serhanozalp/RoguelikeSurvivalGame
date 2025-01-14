using UnityEngine;

[CreateAssetMenu(fileName = "StraightLineMovementStrategy", menuName = "WeaponSystem/Projectile Strategies/StraightLineMovementStrategy")]
public class StraightLineMovementStrategy : BaseWeaponProjectileStrategy
{
    public override void FixedUpdateHook()
    {
        if(_parentProjectile.CanMove) _parentProjectile.transform.Translate(_parentProjectile.WeaponProjectileConfiguration.Speed * Time.fixedDeltaTime * _parentProjectile.transform.forward, Space.World);
    }
}
