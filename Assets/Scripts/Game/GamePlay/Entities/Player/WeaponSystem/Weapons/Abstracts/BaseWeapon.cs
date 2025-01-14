using UnityEngine;

public abstract class BaseWeapon : IInitializable
{
    #region Data Variables

    private BaseWeaponData _weaponData;
    public BaseWeaponData WeaponData { get => _weaponData; }
    private GameObject _weaponGO;

    #endregion

    #region Rate Of Fire Variables

    private CountdownTimer _fireRateCountdownTimer;
    private bool _canFire;

    #endregion

    #region Ammo Variables

    private WeaponProjectileFactory _weaponProjectileFactory;
    private int _currentAmmo;
    private bool _hasAmmo { get => _currentAmmo > 0; }
    private bool _hasMaxAmmo { get => _currentAmmo == _weaponData.MaxAmmo; }
    public bool HasMaxAmmo { get => _hasMaxAmmo; }

    #endregion

    protected BaseWeapon(BaseWeaponData weaponData)
    {
        _weaponData = weaponData;
        Initialize();
    }

    public void Initialize()
    {
        _weaponProjectileFactory = new WeaponProjectileFactory(_weaponData.WeaponProjectileConfiguration);
        _fireRateCountdownTimer = new CountdownTimer(_weaponData.FireRate);
        _fireRateCountdownTimer.TimerStoped += ResetFireCooldown;
        _canFire = true;
        _currentAmmo = _weaponData.MaxAmmo;
    }

    public void Fire()
    {
        if (_canFire && _hasAmmo)
        {
            _canFire = false;
            WeaponProjectile projectile = _weaponProjectileFactory.Create();
            projectile.transform.SetPositionAndRotation(_weaponData.AimIKTransform.position, _weaponData.AimIKTransform.rotation);
            projectile.ShootSequenceAsync().Forget();
            _currentAmmo = Mathf.Max(0, _currentAmmo -= 1);
            _fireRateCountdownTimer.Start();
        }
    }

    public void Reload() => _currentAmmo = _weaponData.MaxAmmo;

    private void ResetFireCooldown() => _canFire = true;

    public void InstantiatePrefab(Transform parent, Vector3 localPosition, Quaternion localRotation)
    {
        _weaponGO = GameObject.Instantiate(_weaponData.WeaponPrefab, parent);
        _weaponGO.transform.SetLocalPositionAndRotation(localPosition, localRotation);
        InitializeIKTransforms();
    }

    private void InitializeIKTransforms()
    {
        if (_weaponGO.TryGetComponent<WeaponIKTransforms>(out WeaponIKTransforms weaponIKTransforms))
        {
            _weaponData.LeftHandIKWeaponGrip = weaponIKTransforms.LeftHandIKWeaponGrip;
            _weaponData.LeftArmIKWeaponBend = weaponIKTransforms.LeftArmIKWeaponBend;
            _weaponData.AimIKTransform = weaponIKTransforms.AimIKTransform;
        }
        else Debug.LogWarning($"{_weaponData.Name} is missing WeaponIKTransforms component!");
    }

    ~BaseWeapon()
    {
        _fireRateCountdownTimer.TimerStoped -= ResetFireCooldown;
    }
}
