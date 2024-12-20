using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerAnimation), typeof(PlayerMovement))]
public class Player : MonoBehaviour , IEntity
{
    private PlayerAnimation _playerAnimation;
    private PlayerMovement _playerMovement;

    private BaseStateMachine<PlayerLocomotionContextData> _playerLocomotionStateMachine;

    private BaseEntityModifierManager _playerEntityModifierManager;
    public BaseEntityModifierManager EntityModifierManager => _playerEntityModifierManager;

    [Inject]
    private void ZenjectConstructor(BaseEntityModifierManager entityModifierManager)
    {
        _playerEntityModifierManager = entityModifierManager;
    }

    private void Awake()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerLocomotionStateMachine = new PlayerLocomotionStateMachine(null, new PlayerLocomotionContextData { PlayerAnimation = _playerAnimation, PlayerMovement = _playerMovement, PlayerTransform = transform });
    }

    private void Update()
    {
        _playerLocomotionStateMachine.OnUpdate();
    }
}
