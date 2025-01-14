using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="WeaponProjectileConfiguration", menuName ="WeaponSystem/WeaponProjectileConfiguration")]
public class WeaponProjectileConfiguration : ScriptableObject
{
    [Header("Behaviour Properties")]
    public float Damage;
    public float Speed;
    public float MaxLifetime;

    [Header("Component Configuration")]
    public TrailRendererConfiguration TrailRendererConfiguration;
    public MeshRendererConfiguration MeshRendererConfiguration;

    [Header("Strategy Configuration")]
    public List<BaseWeaponProjectileStrategy> WeaponProjectileStrategies;
}
