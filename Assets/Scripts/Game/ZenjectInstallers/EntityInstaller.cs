using Zenject;
using UnityEngine;

public class EntityInstaller : MonoInstaller
{
    [SerializeField] private BaseStatsData _statsData;

    public override void InstallBindings()
    {
        Container.Bind<BaseStatsData>().WithId("Base").FromInstance(_statsData).AsCached();
        Container.Bind<BaseStatsData>().WithId("Runtime").FromMethod((context) => Instantiate(context.Container.ResolveId<BaseStatsData>("Base"))).AsCached();
        Container.Bind<IEntity>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<BaseEntityMovement>().FromComponentInHierarchy().AsSingle();
        Container.Bind<BaseEntityAnimation>().FromComponentInHierarchy().AsSingle();
        Container.Bind<EntityMovementStateMachine>().FromNew().AsSingle();
    }
}
