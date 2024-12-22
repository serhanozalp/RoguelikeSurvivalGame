public abstract class BaseLocomotionState : BaseState
{
    protected float _moveSpeed;
    protected BaseEntityMovement _entityMovement;

    protected BaseLocomotionState(BaseState parentState, BaseEntityMovement entitiyMovement) : base(parentState)
    {
        _entityMovement = entitiyMovement;
    }

    public override void Enter()
    {
        SetMovementSpeed();
    }

    private void SetMovementSpeed()
    {
        _entityMovement.MoveSpeed = _moveSpeed;
    }
}
