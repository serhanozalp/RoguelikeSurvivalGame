using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerInput), typeof(PlayerAnimation))]
public class PlayerMovement : BaseEntityMovement
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerAnimation _playerAnimation;
    private Transform _mainCameraTransform;

    private BaseStateMachine _playerLocomotionStateMachine;
    private Vector2 _locomotionBlendTreePoint;

    [Inject]
    private void ZenjectConstructor(BaseStateMachine playerLocomotionStateMachine)
    {
        _playerLocomotionStateMachine = playerLocomotionStateMachine;
    }

    private void Awake()
    {
        _mainCameraTransform = Camera.main.transform;
        _playerLocomotionStateMachine.ChangeState<PlayerIdleState>();
    }

    private void Update()
    {
        Move();
        HandleAnimations();
        LocomotionStateMachineTransitionLogic();
    }

    private void Move()
    {
        Vector3 cameraForward = new Vector3(_mainCameraTransform.forward.x, 0f, _mainCameraTransform.forward.z).normalized;
        Vector3 cameraRight = new Vector3(_mainCameraTransform.right.x, 0f, _mainCameraTransform.right.z).normalized;
        _moveDirection = (cameraForward * _playerInput.MoveZ + cameraRight * _playerInput.MoveX).normalized;
        transform.Translate(_moveDirection * _moveSpeed * _entityStatsData.SpeedModifier.Value * Time.deltaTime, Space.World);
    }

    private void LocomotionStateMachineTransitionLogic()
    {
        switch (Vector2.Angle(_locomotionBlendTreePoint, Vector2.up))
        {
            case float angle when (angle == 0f):
                _playerLocomotionStateMachine.ChangeState<PlayerIdleState>();
                break;
            case float angle when (angle <= 45f && angle > 0f):
                _playerLocomotionStateMachine.ChangeState<PlayerMoveForwardState>();
                break;
            case float angle when (angle >= 135f):
                _playerLocomotionStateMachine.ChangeState<PlayerMoveBackwardState>();
                break;
            case float angle when (angle > 45f && angle < 135f):
                _playerLocomotionStateMachine.ChangeState<PlayerStrafeState>();
                break;
        }
    }

    private void HandleAnimations()
    {
        _locomotionBlendTreePoint = new Vector2(Vector3.Dot(transform.right, _moveDirection), Vector3.Dot(transform.forward, _moveDirection));
        _playerAnimation.SetLocomotionAnimationValues(_locomotionBlendTreePoint.x, _locomotionBlendTreePoint.y);
    }
}
