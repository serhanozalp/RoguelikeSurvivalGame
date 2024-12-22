using UnityEngine;

public class PlayerLocomotionContextData
{
    public BaseEntityAnimation PlayerAnimation;
    public BaseEntityMovement PlayerMovement;
    public Transform PlayerTransform;
}

public class PlayerLocomotionStateMachine : BaseStateMachine<PlayerLocomotionContextData>
{
    private BaseState _idleState;
    private BaseState _strafeState;
    private BaseState _moveForwardState;
    private BaseState _moveBackwardState;

    private Vector2 _locomotionBlendTreePoint;

    public PlayerLocomotionStateMachine(BaseState parentState, PlayerLocomotionContextData contextData) : base(parentState, contextData)
    {
        _idleState = new PlayerIdleState(this);
        _strafeState = new PlayerStrafeState(this, _contextData.PlayerMovement);
        _moveForwardState = new PlayerMoveForwardState(this, _contextData.PlayerMovement);
        _moveBackwardState = new PlayerMoveBackwardState(this, _contextData.PlayerMovement);
        ChangeState(_idleState);
    }

    public override void OnUpdate()
    {
        HandleAnimations();
        base.OnUpdate();
    }

    private void HandleAnimations()
    {
        _locomotionBlendTreePoint = new Vector2(Vector3.Dot(_contextData.PlayerTransform.right, _contextData.PlayerMovement.MoveDirection), Vector3.Dot(_contextData.PlayerTransform.forward, _contextData.PlayerMovement.MoveDirection));
        (_contextData.PlayerAnimation as PlayerAnimation).SetLocomotionAnimationValues(_locomotionBlendTreePoint.x, _locomotionBlendTreePoint.y);
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
