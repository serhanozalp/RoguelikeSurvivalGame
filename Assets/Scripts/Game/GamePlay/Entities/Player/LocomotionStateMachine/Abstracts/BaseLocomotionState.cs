using Zenject;

public abstract class BaseLocomotionState : BaseState
{
    protected float _moveSpeed;
    
    protected BaseEntityMovement _entityMovement;

    [Inject]
    protected void ZenjectConstructor(BaseEntityMovement entityMovement)
    {
        _entityMovement = entityMovement;
    }

    protected BaseLocomotionState(BaseStateMachine parentStateMachine) : base(parentStateMachine)
    {
    }

    public override void Enter()
    {
        SetMovementSpeed();
    }

    private void SetMovementSpeed() => _entityMovement.MoveSpeed = _moveSpeed;
}
