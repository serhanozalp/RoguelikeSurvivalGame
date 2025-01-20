using Cysharp.Threading.Tasks;
using RootMotion.FinalIK;
using Zenject;

public class PlayerAnimation : BaseEntityAnimation
{
    private PlayerWeaponManager _playerWeaponManager;
    private PlayerIKFacade _playerIKFacade;

    [Inject]
    private void ZenjectConstructor(PlayerWeaponManager playerWeaponManager, PlayerIKFacade playerIKFacade)
    {
        _playerWeaponManager = playerWeaponManager;
        _playerIKFacade = playerIKFacade;
    }

    private void Update()
    {
        _playerIKFacade.UpdateHook();
    }

    public async UniTask PlayWeaponEquipAnimation()
    {
        _entityAnimator.runtimeAnimatorController = _playerWeaponManager.CurrentWeapon.WeaponData.WeaponAOC;
        _entityAnimator.SetLayerWeight(GameConstants.Player.Animation.ARMS_LAYER_INDEX, 1);
        _playerIKFacade.SetupIKForWeaponEquip();
        _entityAnimator.CrossFadeInFixedTime(GameConstants.Player.Animation.ANIMATION_STATE_NAME_WEAPON_GRAB, GameConstants.Player.Animation.ANIMATION_FADE_DURATION);
        await UniTask.WaitUntil(() => IsAnimationFinished(GameConstants.Player.Animation.ARMS_LAYER_INDEX, GameConstants.Player.Animation.ANIMATION_STATE_NAME_WEAPON_GRAB));
    }

    public async UniTask PlayWeaponIdleAnimation()
    {
        _entityAnimator.CrossFadeInFixedTime(GameConstants.Player.Animation.ANIMATION_STATE_NAME_WEAPON_IDLE, GameConstants.Player.Animation.ANIMATION_FADE_DURATION);
        BaseWeaponData weaponData = _playerWeaponManager.CurrentWeapon.WeaponData;
        await UniTask.WhenAll(
            _playerIKFacade.MoveLimbToAndFollow(FullBodyBipedChain.LeftArm, weaponData.LeftHandIKWeaponGrip, GameConstants.Player.Animation.ANIMATION_FADE_DURATION, weaponData.LeftArmIKWeaponBend),
            _playerIKFacade.UnAimWeapon(GameConstants.Player.Animation.ANIMATION_FADE_DURATION));
    }

    public async UniTask PlayWeaponAimAnimation()
    {
        _entityAnimator.CrossFadeInFixedTime(GameConstants.Player.Animation.ANIMATION_STATE_NAME_WEAPON_AIM, GameConstants.Player.Animation.ANIMATION_FADE_DURATION);
        await _playerIKFacade.AimWeapon(_playerWeaponManager.CurrentWeapon.WeaponData.AimIKTransform, GameConstants.Player.Animation.ANIMATION_FADE_DURATION);
    }

    public async UniTask PlayWeaponReloadAnimation()
    {
        _playerIKFacade.SetupIKForWeaponReload();
        _entityAnimator.CrossFadeInFixedTime(GameConstants.Player.Animation.ANIMATION_STATE_NAME_WEAPON_RELOAD, GameConstants.Player.Animation.ANIMATION_FADE_DURATION);
        await UniTask.WhenAll(
            UniTask.WaitUntil(() => IsAnimationFinished(GameConstants.Player.Animation.ARMS_LAYER_INDEX, GameConstants.Player.Animation.ANIMATION_STATE_NAME_WEAPON_RELOAD)),
            _playerIKFacade.UnAimWeapon(GameConstants.Player.Animation.ANIMATION_FADE_DURATION));
    }
}
