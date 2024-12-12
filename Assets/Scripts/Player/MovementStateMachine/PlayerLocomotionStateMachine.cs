using UnityEngine;

public class LocomotionContextData
{
    public PlayerAnimation PlayerAnimation;
    public PlayerMovement PlayerMovement;
    public Transform PlayerTransform;
}

public class PlayerLocomotionStateMachine : BaseStateMachine<LocomotionContextData>
{
    private BaseState<LocomotionContextData> _idleState;
    private BaseState<LocomotionContextData> _strafeState;
    private BaseState<LocomotionContextData> _moveForwardState;
    private BaseState<LocomotionContextData> _moveBackwardState;

    private Vector2 _locomotionBlendTreePoint;

    public PlayerLocomotionStateMachine(BaseState<LocomotionContextData> parentState, LocomotionContextData contextData) : base(parentState, contextData)
    {
        _idleState = new PlayerIdleState(this, _contextData);
        _strafeState = new PlayerStrafeState(this, _contextData);
        _moveForwardState = new PlayerMoveForwardState(this, _contextData);
        _moveBackwardState = new PlayerMoveBackwardState(this, _contextData);
        ChangeState(_idleState);
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Player entered Locomotion StateMachine");
    }

    public override void Exit()
    {
        Debug.Log("Player exited Locomotion StateMachine");
    }

    public override void OnUpdate()
    {
        _locomotionBlendTreePoint = new Vector2(Vector3.Dot(_contextData.PlayerTransform.right, _contextData.PlayerMovement.MoveDirection), Vector3.Dot(_contextData.PlayerTransform.forward, _contextData.PlayerMovement.MoveDirection));
        base.OnUpdate();
    }
    public override void OnLateUpdate()
    {
        base.OnLateUpdate();
        HandleAnimations();
    }

    protected override void HandleAnimations()
    {
        _contextData.PlayerAnimation.SetLocomotionAnimationValues(_locomotionBlendTreePoint.x, _locomotionBlendTreePoint.y);
    }

    protected override void StateTransitionLogic()
    {
        switch (Vector2.Angle(_locomotionBlendTreePoint, Vector2.up))
        {
            case float angle when (angle == 0f):
                ChangeState(_idleState);
                break;
            case float angle when (angle <= 45f && angle > 0f):
                ChangeState(_moveForwardState);
                break;
            case float angle when (angle >= 135f):
                ChangeState(_moveBackwardState);
                break;
            case float angle when (angle > 45f && angle < 135f):
                ChangeState(_strafeState);
                break;
        }
    }
}
