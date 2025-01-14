public class WeaponEquipingState : BaseWeaponState
{
    public WeaponEquipingState(BaseStateMachine parentStateMachine) : base(parentStateMachine)
    {
    }

    public override async void Enter()
    {
        using (new SequenceScope(
           () => _parentStateMachine.IsStateTransitionLocked = true,
           () => _parentStateMachine.IsStateTransitionLocked = false))
        {
            _playerWeaponManager.ClearWeaponHolder();
            _playerWeaponManager.CurrentWeapon = (_parentStateMachine as PlayerWeaponStateMachine).WeaponToEquip;
            _playerWeaponManager.CurrentWeapon.InstantiatePrefab(_playerWeaponManager.WeaponHolder, _playerWeaponManager.CurrentWeapon.WeaponData.WeaponHolderLocalPosition, _playerWeaponManager.CurrentWeapon.WeaponData.WeaponHolderLocalRotation);
            await _playerAnimation.PlayWeaponEquipAnimation();
        }
        _parentStateMachine.ChangeState<WeaponIdleState>();
    }
}
