using UnityEngine;
using Zenject;


public class PlayerMovement : BaseEntityMovement
{
    private PlayerInput _playerInput;
    private Transform _mainCameraTransform;

    [Inject]
    private void ZenjectConstructor(PlayerInput playerInput)
    {
        _playerInput = playerInput;
    }

    private void Awake()
    {
        _mainCameraTransform = Camera.main.transform;
    }

    protected override void Update()
    {
        base.Update();
        Move(CalculateDestination());
    }

    public override void Move(Vector3 destination) => transform.Translate(destination, Space.World);

    private Vector3 CalculateDestination()
    {
        Vector3 cameraForward = new Vector3(_mainCameraTransform.forward.x, 0f, _mainCameraTransform.forward.z).normalized;
        Vector3 cameraRight = new Vector3(_mainCameraTransform.right.x, 0f, _mainCameraTransform.right.z).normalized;
        _moveDirection = (cameraForward * _playerInput.MoveZ + cameraRight * _playerInput.MoveX).normalized;
        return _moveSpeed * Time.deltaTime * _moveDirection;
    }
}
