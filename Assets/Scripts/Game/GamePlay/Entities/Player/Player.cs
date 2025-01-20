using UnityEngine;
using Zenject;

public class Player : MonoBehaviour , IEntity
{
    private BaseEntityModifierManager _playerEntityModifierManager;
    public BaseEntityModifierManager EntityModifierManager => _playerEntityModifierManager;

    [Inject]
    private void ZenjectConstructor(BaseEntityModifierManager entityModifierManager)
    {
        _playerEntityModifierManager = entityModifierManager;
    }
}
