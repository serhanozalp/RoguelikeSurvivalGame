using UnityEngine;

public class Enemy : MonoBehaviour, IEntity
{
    private BaseEntityModifierManager _enemyEntityModifierManager;
    public BaseEntityModifierManager EntityModifierManager { get => _enemyEntityModifierManager; }  
}
