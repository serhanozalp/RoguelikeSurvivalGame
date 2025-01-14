using UnityEngine;

public abstract class BaseWeaponData : ScriptableObject
{
    [Header("Weapon Identity")]
    public string Name;
    public AnimatorOverrideController WeaponAOC;
    public GameObject WeaponPrefab;

    [Header("Weapon Transform Properties")]
    public Vector3 WeaponHolderLocalPosition;
    public Quaternion WeaponHolderLocalRotation;

    [Header("Weapon Projectile Configuration")]
    public WeaponProjectileConfiguration WeaponProjectileConfiguration;

    [Header("Weapon Properties")]
    public int MaxAmmo;
    public float FireRate;

    [HideInInspector] public Transform LeftHandIKWeaponGrip;
    [HideInInspector] public Transform LeftArmIKWeaponBend;
    [HideInInspector] public Transform AimIKTransform;
}

