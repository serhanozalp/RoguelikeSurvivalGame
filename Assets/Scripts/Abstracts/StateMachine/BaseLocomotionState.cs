public abstract class BaseLocomotionState<T> : BaseState<T> where T : class
{
    protected float _moveSpeed;

    protected BaseLocomotionState(BaseState<T> parentState, T contextData) : base(parentState, contextData)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetMovementSpeed();
    }

    protected abstract void SetMovementSpeed();
}
