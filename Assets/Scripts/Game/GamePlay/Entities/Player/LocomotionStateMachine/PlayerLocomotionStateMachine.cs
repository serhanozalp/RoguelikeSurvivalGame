using Zenject;

public class PlayerLocomotionStateMachine : BaseStateMachine
{
    private BaseState _playerIdleState;
    private BaseState _playerStrafeState;
    private BaseState _playerMoveForwardSate;
    private BaseState _playerMoveBackwardState;

    [Inject]
    private void ZenjectConstructor(DiContainer container)
    {
        container.QueueForInject(_playerStrafeState);
        container.QueueForInject(_playerMoveForwardSate);
        container.QueueForInject(_playerMoveBackwardState);
    }

    public PlayerLocomotionStateMachine(BaseStateMachine parentStateMachine = null) : base(parentStateMachine)
    {
        _playerIdleState = new PlayerIdleState(this);
        _playerStrafeState = new PlayerStrafeState(this);
        _playerMoveBackwardState = new PlayerMoveBackwardState(this);
        _playerMoveForwardSate = new PlayerMoveForwardState(this);
        AddState(_playerIdleState as PlayerIdleState);
        AddState(_playerStrafeState as PlayerStrafeState);
        AddState(_playerMoveForwardSate as PlayerMoveForwardState);
        AddState(_playerMoveBackwardState as PlayerMoveBackwardState);
    }
}
