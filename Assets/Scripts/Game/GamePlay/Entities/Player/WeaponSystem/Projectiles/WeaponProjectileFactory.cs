using UnityEngine;
using UnityEngine.Pool;

public class WeaponProjectileFactory : IFactory<WeaponProjectile>
{
    private readonly ObjectPool<WeaponProjectile> _projectilePool;
    private readonly WeaponProjectileConfiguration _weaponProjectileConfiguration;

    public WeaponProjectileFactory(WeaponProjectileConfiguration weaponProjectileConfiguration)
    {
        _weaponProjectileConfiguration = weaponProjectileConfiguration;
        _projectilePool = new ObjectPool<WeaponProjectile>(CreateProjectilePF, (projectile) => projectile.gameObject.SetActive(true), (projectile) => projectile.Reset(), collectionCheck: false);
    }

    private WeaponProjectile CreateProjectilePF()
    {
        GameObject projectileGO = new GameObject("WeaponProjectile");
        WeaponProjectile projectileComponent = projectileGO.AddComponent<WeaponProjectile>();
        return ConfigureProjectile(projectileComponent);
    }

    private WeaponProjectile ConfigureProjectile(WeaponProjectile projectile)
    {
        ConfigureTrailRenderer(projectile);
        projectile.Initialize(_projectilePool, _weaponProjectileConfiguration);
        return projectile;
    }

    private void ConfigureTrailRenderer(WeaponProjectile projectile)
    {
        if (_weaponProjectileConfiguration.TrailRendererConfiguration != null)
        {
            TrailRenderer trailRenderer = projectile.gameObject.AddComponent<TrailRenderer>();
            trailRenderer.widthCurve = _weaponProjectileConfiguration.TrailRendererConfiguration.WidthCurve;
            trailRenderer.time = _weaponProjectileConfiguration.TrailRendererConfiguration.Time;
            trailRenderer.colorGradient = _weaponProjectileConfiguration.TrailRendererConfiguration.Color;
            trailRenderer.material = _weaponProjectileConfiguration.TrailRendererConfiguration.Material;
        }
    }

    public WeaponProjectile Create() => _projectilePool.Get();
}
