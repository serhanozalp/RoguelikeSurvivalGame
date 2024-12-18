using UnityEngine;

public class PlayerIdleState : BaseLocomotionState<LocomotionContextData>
{
    public PlayerIdleState(BaseState<LocomotionContextData> parentState, LocomotionContextData contextData) : base(parentState, contextData)
    {
        _moveSpeed = 0f;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
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
