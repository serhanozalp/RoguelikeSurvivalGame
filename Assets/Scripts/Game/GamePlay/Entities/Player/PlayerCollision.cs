using UnityEngine;

[RequireComponent(typeof(Collider), typeof(IEntity))]
public class PlayerCollision : MonoBehaviour
{
    private IEntity _playerEntity;

    private void Awake()
    {
        _playerEntity = GetComponent<IEntity>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<IPickup>(out IPickup pickupObject))
        {
            pickupObject.Pickup(_playerEntity);
        }
    }
}
