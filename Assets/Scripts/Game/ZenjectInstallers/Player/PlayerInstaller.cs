using Zenject;
using UnityEngine;
using RootMotion.FinalIK;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerAnimation _playerAnimation;
    [SerializeField] private BaseEntityMovement _playerMovement;
    [SerializeField] private PlayerWeaponManager _playerWeaponManager;
    [SerializeField] private PlayerInput _playerInput;

    [SerializeField] private FullBodyBipedIK _playerFullBodyBipedIK;
    [SerializeField] private AimIK _playerWeaponAimIK;
    
    public override void InstallBindings()
    {
        Container.Bind<BaseStatsData>().WithId("Base").To<PlayerStatsData>().FromScriptableObjectResource("ScriptableObjects/Data/Stats/Player/PlayerStatsData").AsSingle();
        Container.Bind<BaseStatsData>().WithId("Runtime").To<PlayerStatsData>().FromMethod(() => 
        {
            return ScriptableObject.Instantiate(Container.ResolveId<BaseStatsData>("Base") as PlayerStatsData);
        }).AsCached();

        Container.Bind<BaseEntityModifierManager>().To<PlayerModifierManager>().AsSingle();
        Container.Bind<PlayerAnimation>().FromInstance(_playerAnimation).AsSingle();
        Container.Bind<BaseEntityMovement>().FromInstance(_playerMovement).AsSingle();
        Container.Bind<PlayerWeaponManager>().FromInstance(_playerWeaponManager).AsSingle();
        Container.Bind<PlayerInput>().FromInstance(_playerInput).AsSingle();


        Container.Bind<BaseStateMachine>().To<PlayerLocomotionStateMachine>().FromNew().AsSingle().WhenInjectedInto<PlayerMovement>();
        Container.Bind<IWeaponStateMachine>().To<PlayerWeaponStateMachine>().FromNew().AsSingle();

        Container.Bind<PlayerIKFacade>().AsSingle();
        Container.Bind<FullBodyBipedIK>().FromInstance(_playerFullBodyBipedIK).AsSingle();
        Container.Bind<AimIK>().FromInstance(_playerWeaponAimIK).AsSingle();
    }
}