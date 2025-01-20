using UnityEngine;
using Zenject;

public class PlayerCollision : MonoBehaviour
{
    private IEntity _playerEntity;

    [Inject]
    private void ZenjectConstructor(IEntity playerEntity)
    {
        _playerEntity = playerEntity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IPickup>(out IPickup pickupObject))
        {
            pickupObject.Pickup(_playerEntity);
        }
    }
}
