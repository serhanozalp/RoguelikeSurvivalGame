using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerRotation : MonoBehaviour
{
    private Camera _mainCamera;
    private PlayerInput _playerInput;

    private float _rotateSpeed = 5f;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        Vector3 mousePosition = _playerInput.MousePosition;
        mousePosition.z = _mainCamera.WorldToScreenPoint(transform.position).z;
        Vector3 worldMousePosition = _mainCamera.ScreenToWorldPoint(mousePosition);
        Vector3 directionToMouse = worldMousePosition - transform.position;
        directionToMouse.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(directionToMouse);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
    }
}
