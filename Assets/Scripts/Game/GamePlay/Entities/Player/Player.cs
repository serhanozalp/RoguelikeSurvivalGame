using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerAnimation), typeof(PlayerMovement))]
public class Player : MonoBehaviour , IEntity
{
    private BaseEntityAnimation _playerAnimation;
    private BaseEntityMovement _playerMovement;

    private BaseStateMachine<PlayerLocomotionContextData> _playerLocomotionStateMachine;

    private BaseEntityModifierManager _playerEntityModifierManager;
    public BaseEntityModifierManager EntityModifierManager => _playerEntityModifierManager;

    [Inject]
    private void ZenjectConstructor(BaseEntityAnimation entityAnimation, BaseEntityMovement entityMovement, BaseEntityModifierManager entityModifierManager)
    {
        _playerAnimation = entityAnimation;
        _playerMovement = entityMovement;
        _playerEntityModifierManager = entityModifierManager;
    }

    private void Awake()
    {
        _playerLocomotionStateMachine = new PlayerLocomotionStateMachine(null, new PlayerLocomotionContextData { PlayerAnimation = _playerAnimation, PlayerMovement = _playerMovement, PlayerTransform = transform });
    }

    private void Update()
    {
        _playerLocomotionStateMachine.OnUpdate();
    }
}
