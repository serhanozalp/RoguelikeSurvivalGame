using UnityEngine;

[RequireComponent(typeof(Collider), typeof(IEntity))]
public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<IPickup>(out IPickup pickupObject))
        {
            pickupObject.Pickup(GetComponent<IEntity>());
        }
    }
}
