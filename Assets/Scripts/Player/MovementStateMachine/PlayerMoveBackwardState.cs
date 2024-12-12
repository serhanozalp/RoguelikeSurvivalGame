using UnityEngine;

public class PlayerMoveBackwardState : BaseLocomotionState<LocomotionContextData>
{
    public PlayerMoveBackwardState(BaseState<LocomotionContextData> parentState, LocomotionContextData contextData) : base(parentState, contextData)
    {
        _moveSpeed = 2f;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Player entered MoveBackward State");
    }

    public override void Exit()
    {
        Debug.Log("Player exited MoveBackward State");
    }

    public override void OnLateUpdate()
    {
    }

    public override void OnUpdate()
    {
    }

    protected override void SetMovementSpeed()
    {
        _contextData.PlayerMovement.MoveSpeed = _moveSpeed;
    }
}
