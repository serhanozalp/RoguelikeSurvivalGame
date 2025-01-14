using UnityEngine;
using Zenject;

public class PlayerWeaponManager : MonoBehaviour
{
    #region Component Variables

    private PlayerInput _playerInput;

    #endregion

    #region IK Variables

    [SerializeField] private Transform _aimIKTarget;
    public Transform AimIKTarget { get => _aimIKTarget; }

    #endregion

    #region Weapon Variables

    [SerializeField] private Transform _weaponHolder;
    public Transform WeaponHolder { get { return _weaponHolder; } }
    private BaseWeapon _currentWeapon;
    public BaseWeapon CurrentWeapon { get { return _currentWeapon; } set => _currentWeapon = value; }
    private IWeaponStateMachine _weaponStateMachine;

    #endregion
    
    private DiContainer _container;

    [Inject]
    private void ZenjectConstructor(DiContainer container, IWeaponStateMachine weaponStateMachine, PlayerInput playerInput)
    {
        _container = container;
        _weaponStateMachine = weaponStateMachine;
        _playerInput = playerInput;
    }

    private void Update()
    {
        #region Test
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(_container.Resolve<RevolverWeapon>());
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(_container.Resolve<RifleWeapon>());
        }
        #endregion

        if (_playerInput.IsKeyDownR) ReloadWeapon();

        (_weaponStateMachine as BaseStateMachine).OnUpdate();
    }

    public void EquipWeapon(BaseWeapon weapon) => _weaponStateMachine.EquipWeapon(weapon);
    private void ReloadWeapon() => _weaponStateMachine.ReloadWeapon();

    public void ClearWeaponHolder()
    {
        foreach (Transform child in _weaponHolder) Destroy(child.gameObject);
    }
}
