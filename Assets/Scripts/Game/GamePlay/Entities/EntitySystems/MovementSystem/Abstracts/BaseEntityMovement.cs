using UnityEngine;
using Zenject;

public abstract class BaseEntityMovement : MonoBehaviour
{
    protected Vector3 _moveDirection;
    public Vector3 MoveDirection { get => _moveDirection; }

    protected float _moveSpeed;
    public virtual float MoveSpeed { get => _moveSpeed; set { _moveSpeed = value; } }

    private EntityMovementStateMachine _entityMovementStateMachine;
    private BaseEntityAnimation _entityAnimation;
    private Vector2 _locomotionBlendTreePoint;

    [Inject]
    private void ZenjectConstructor(EntityMovementStateMachine entityMovementStateMachine, BaseEntityAnimation entityAnimation)
    {
        _entityMovementStateMachine = entityMovementStateMachine;
        _entityAnimation = entityAnimation;
    }

    private void Start()
    {
        _entityMovementStateMachine.ChangeState<IdleMovementState>();
    }

    protected virtual void Update()
    {
        HandleAnimations();
        LocomotionStateMachineTransitionLogic();
    }

    public abstract void Move(Vector3 destination);

    private void LocomotionStateMachineTransitionLogic()
    {
        switch (Vector2.Angle(_locomotionBlendTreePoint, Vector2.up))
        {
            case float angle when (angle == 0f):
                _entityMovementStateMachine.ChangeState<IdleMovementState>();
                break;
            case float angle when (angle <= 45f && angle > 0f):
                _entityMovementStateMachine.ChangeState<MoveForwardState>();
                break;
            case float angle when (angle >= 135f):
                _entityMovementStateMachine.ChangeState<MoveBackwardState>();
                break;
            case float angle when (angle > 45f && angle < 135f):
                _entityMovementStateMachine.ChangeState<StrafeState>();
                break;
        }
    }

    private void HandleAnimations()
    {
        _locomotionBlendTreePoint = new Vector2(Vector3.Dot(transform.right, _moveDirection), Vector3.Dot(transform.forward, _moveDirection));
        _entityAnimation.SetLocomotionAnimationValues(_locomotionBlendTreePoint.x, _locomotionBlendTreePoint.y);
    }
}
