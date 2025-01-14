using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Pool;

public class WeaponProjectile : MonoBehaviour , IReset
{
    private ObjectPool<WeaponProjectile> _projectilePool;
    public ObjectPool<WeaponProjectile> ProjectilePool { get => _projectilePool; }
    private WeaponProjectileConfiguration _weaponProjectileConfiguration;
    public WeaponProjectileConfiguration WeaponProjectileConfiguration { get => _weaponProjectileConfiguration; }
    private TrailRenderer _trailRenderer;
    private List<BaseWeaponProjectileStrategy> _weaponProjectileStrategies;

    private CancellationTokenSource _maxLifetimeCTS;

    public bool CanMove;

    public void Initialize(ObjectPool<WeaponProjectile> projectilePool, WeaponProjectileConfiguration weaponProjectileConfiguration)
    {
        _projectilePool = projectilePool;
        _weaponProjectileConfiguration = weaponProjectileConfiguration;
        _trailRenderer = GetComponent<TrailRenderer>();
        InitializeStrategies();
    }

    private void InitializeStrategies()
    {
        _weaponProjectileStrategies = new List<BaseWeaponProjectileStrategy>();
        foreach (BaseWeaponProjectileStrategy weaponProjectileStrategy in _weaponProjectileConfiguration.WeaponProjectileStrategies)
        {
            BaseWeaponProjectileStrategy cloneStrategy = ScriptableObject.Instantiate(weaponProjectileStrategy);
            cloneStrategy.Initialize(this);
            _weaponProjectileStrategies.Add(cloneStrategy);
        }
    }

    private void FixedUpdate()
    {
        foreach (BaseWeaponProjectileStrategy weaponProjectileStrategy in _weaponProjectileStrategies) weaponProjectileStrategy.FixedUpdateHook();
    }

    public async UniTaskVoid ShootSequenceAsync()
    {
        using (new SequenceScope(
            setupAction: () =>
            {
                CanMove = true;
                _maxLifetimeCTS = new CancellationTokenSource();
            },
            cleanUpAction: () =>
            {
                _maxLifetimeCTS?.Dispose();
            }))
        {
            await UniTask.WaitForSeconds(_weaponProjectileConfiguration.MaxLifetime, cancellationToken: _maxLifetimeCTS.Token);
            if (!_maxLifetimeCTS.IsCancellationRequested) _projectilePool.Release(this);
        }
    }

    public void Reset()
    {
        _maxLifetimeCTS?.Cancel();
        _trailRenderer.Clear();
        gameObject.SetActive(false);
    }

    private void OnDestroy() => _maxLifetimeCTS?.Dispose();
}
