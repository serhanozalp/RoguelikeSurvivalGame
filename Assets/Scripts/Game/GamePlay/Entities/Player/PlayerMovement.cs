using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : BaseEntityMovement
{
    private PlayerInput _playerInput;
    private Transform _mainCameraTransform;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _mainCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 cameraForward = new Vector3(_mainCameraTransform.forward.x, 0f, _mainCameraTransform.forward.z).normalized;
        Vector3 cameraRight = new Vector3(_mainCameraTransform.right.x, 0f, _mainCameraTransform.right.z).normalized;
        _moveDirection = (cameraForward * _playerInput.MoveZ + cameraRight * _playerInput.MoveX).normalized;
        transform.Translate(_moveDirection * _moveSpeed * _entityStatsData.SpeedModifier.Value * Time.deltaTime, Space.World);
    }
}
