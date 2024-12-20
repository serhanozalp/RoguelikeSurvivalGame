using Zenject;
using UnityEngine;

public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<BaseStatsData>().WithId("Base").To<PlayerStatsData>().FromScriptableObjectResource("StatsData/Player/PlayerStatsData").AsSingle();
        Container.Bind<BaseStatsData>().WithId("Runtime").To<PlayerStatsData>().FromMethod(() => 
        {
            return ScriptableObject.Instantiate(Container.ResolveId<BaseStatsData>("Base") as PlayerStatsData);
        }).AsCached();
        Container.Bind<BaseEntityModifierManager>().To<PlayerEntityModifierManager>().AsSingle().WithArguments(Container.ResolveId<BaseStatsData>("Base"), Container.ResolveId<BaseStatsData>("Runtime"));
    }
}
