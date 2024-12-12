using UnityEngine;

public class PlayerMoveForwardState : BaseLocomotionState<LocomotionContextData>
{
    public PlayerMoveForwardState(BaseState<LocomotionContextData> parentState, LocomotionContextData contextData) : base(parentState, contextData)
    {
        _moveSpeed = 5f;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Player entered MoveForward State");
    }

    public override void Exit()
    {
        Debug.Log("Player exited MoveForward State");
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
