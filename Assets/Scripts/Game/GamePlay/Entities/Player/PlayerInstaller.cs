using Zenject;
using UnityEngine;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private BaseEntityAnimation _playerAnimation;
    [SerializeField] private BaseEntityMovement _playerMovement;
    
    public override void InstallBindings()
    {
        Container.Bind<BaseStatsData>().WithId("Base").To<PlayerStatsData>().FromScriptableObjectResource("StatsData/Player/PlayerStatsData").AsSingle();
        Container.Bind<BaseStatsData>().WithId("Runtime").To<PlayerStatsData>().FromMethod(() => 
        {
            return ScriptableObject.Instantiate(Container.ResolveId<BaseStatsData>("Base") as PlayerStatsData);
        }).AsCached();
        Container.Bind<BaseEntityModifierManager>().To<PlayerEntityModifierManager>().AsSingle();
        Container.Bind<BaseEntityAnimation>().FromInstance(_playerAnimation).AsSingle();
        Container.Bind<BaseEntityMovement>().FromInstance(_playerMovement).AsSingle();
    }
}