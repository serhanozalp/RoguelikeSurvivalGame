using Zenject;

public abstract class BaseWeaponState : BaseState
{
    protected PlayerAnimation _playerAnimation;
    protected PlayerWeaponManager _playerWeaponManager;
    protected PlayerInput _playerInput;

    protected BaseWeaponState(BaseStateMachine parentStateMachine) : base(parentStateMachine)
    {
    }

    [Inject]
    protected void ZenjectConstructor(PlayerAnimation playerAnimation, PlayerWeaponManager playerWeaponManager, PlayerInput playerInput)
    {
        _playerAnimation = playerAnimation;
        _playerWeaponManager = playerWeaponManager;
        _playerInput = playerInput;
    }
}
