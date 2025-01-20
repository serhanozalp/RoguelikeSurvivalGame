using Zenject;

public class WeaponsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<BaseWeaponData>().To<RevolverWeaponData>().FromNewScriptableObjectResource("ScriptableObjects/Data/Weapons/Revolver/RevolverWeaponData").AsSingle().WhenInjectedInto<RevolverWeapon>();
        Container.Bind<RevolverWeapon>().FromNew().AsSingle().NonLazy();

        Container.Bind<BaseWeaponData>().To<RifleWeaponData>().FromNewScriptableObjectResource("ScriptableObjects/Data/Weapons/Rifle/RifleWeaponData").AsSingle().WhenInjectedInto<RifleWeapon>();
        Container.Bind<RifleWeapon>().FromNew().AsSingle().NonLazy();
    }
}
