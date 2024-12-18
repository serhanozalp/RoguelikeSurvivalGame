using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Transform _mainCameraTransform;

    private Vector3 _moveDirection;
    public Vector3 MoveDirection { get { return _moveDirection; } }

    private float _moveSpeed;
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

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
        transform.Translate(_moveDirection * _moveSpeed * Time.deltaTime, Space.World);
    }
}
