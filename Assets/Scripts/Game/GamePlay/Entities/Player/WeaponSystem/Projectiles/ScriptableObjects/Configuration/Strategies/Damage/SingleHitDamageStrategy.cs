using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "SingleHitDamageStrategy", menuName = "WeaponSystem/Projectile Strategies/SingleHitDamageStrategy")]
public class SingleHitDamageStrategy : BaseWeaponProjectileStrategy
{
    [SerializeField] private LayerMask _layerMask;
    private RaycastHit _hitInfo;
    private bool _isHandlingImpact;
    
    public override void FixedUpdateHook()
    {
        if (_isHandlingImpact) return;
        if(Physics.Raycast(_parentProjectile.transform.position, _parentProjectile.transform.forward, out _hitInfo, _parentProjectile.WeaponProjectileConfiguration.Speed * Time.fixedDeltaTime, _layerMask))
        {
            ImpactSequenceAsync().Forget();
        }
    }

    private async UniTaskVoid ImpactSequenceAsync()
    {
        using (new SequenceScope(
            setupAction: () =>
            {
                _isHandlingImpact = true;
                _parentProjectile.CanMove = false;
            },
            cleanUpAction: () =>
            {
                _isHandlingImpact = false;
            }))
        {
            await UniTask.WaitForFixedUpdate();
            Impact();
            await UniTask.WaitForSeconds(_parentProjectile.WeaponProjectileConfiguration.TrailRendererConfiguration.Time);
            _parentProjectile.ProjectilePool.Release(_parentProjectile);
        }
    }

    private void Impact()
    {
        _parentProjectile.transform.position = _hitInfo.point;
        if (_hitInfo.collider.TryGetComponent<IDamageable>(out IDamageable damagable)) damagable.ApplyDamage(_parentProjectile.WeaponProjectileConfiguration.Damage);
    }
}
