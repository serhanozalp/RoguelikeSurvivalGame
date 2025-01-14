using Zenject;

public class PlayerWeaponStateMachine : BaseStateMachine , IWeaponStateMachine
{
    private BaseState _weaponEquipState;
    private BaseState _weaponIdleState;
    private BaseState _weaponAimingState;
    private BaseState _weaponReloadingState;

    private PlayerWeaponManager _playerWeaponManager;
    private BaseWeapon _weaponToEquip;
    public BaseWeapon WeaponToEquip { get => _weaponToEquip; }

    [Inject]
    private void ZenjectConstructor(DiContainer container, PlayerWeaponManager playerWeaponManager)
    {
        _playerWeaponManager = playerWeaponManager;

        container.QueueForInject(_weaponEquipState);
        container.QueueForInject(_weaponIdleState);
        container.QueueForInject(_weaponAimingState);
        container.QueueForInject(_weaponReloadingState);
    }

    public PlayerWeaponStateMachine(BaseStateMachine parentStateMachine = null) : base(parentStateMachine)
    {
        _weaponEquipState = new WeaponEquipingState(this);
        _weaponIdleState = new WeaponIdleState(this);
        _weaponAimingState = new WeaponAimingState(this);
        _weaponReloadingState = new WeaponReloadingState(this);
        AddState(_weaponEquipState as WeaponEquipingState);
        AddState(_weaponIdleState as WeaponIdleState);
        AddState(_weaponAimingState as WeaponAimingState);
        AddState(_weaponReloadingState as WeaponReloadingState);
    }

    public void EquipWeapon(BaseWeapon weaponToEquip)
    {
        if (weaponToEquip == _playerWeaponManager.CurrentWeapon) return;
        _weaponToEquip = weaponToEquip;
        ChangeState<WeaponEquipingState>();
    }

    public void ReloadWeapon()
    {
        if (_playerWeaponManager.CurrentWeapon == null || _playerWeaponManager.CurrentWeapon.HasMaxAmmo) return;
        ChangeState<WeaponReloadingState>();
    }
}
