using Zenject;
using UnityEngine;
using RootMotion.FinalIK;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerWeaponManager _playerWeaponManager;
    [SerializeField] private FullBodyBipedIK _playerFullBodyBipedIK;
    [SerializeField] private AimIK _playerWeaponAimIK;
    
    public override void InstallBindings()
    {
        Container.Bind<BaseEntityModifierManager>().To<PlayerModifierManager>().AsSingle();
        Container.Bind<PlayerWeaponManager>().FromInstance(_playerWeaponManager).AsSingle();
        Container.Bind<PlayerInput>().FromInstance(_playerInput).AsSingle();
        Container.Bind<IWeaponStateMachine>().To<PlayerWeaponStateMachine>().FromNew().AsSingle();
        Container.Bind<PlayerIKFacade>().AsSingle();
        Container.Bind<FullBodyBipedIK>().FromInstance(_playerFullBodyBipedIK).AsSingle();
        Container.Bind<AimIK>().FromInstance(_playerWeaponAimIK).AsSingle();
    }
}