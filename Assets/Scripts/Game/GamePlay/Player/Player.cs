using UnityEngine;

[RequireComponent(typeof(PlayerAnimation), typeof(PlayerMovement))]
public class Player : MonoBehaviour , IEntity
{
    private PlayerAnimation _playerAnimation;
    private PlayerMovement _playerMovement;

    private BaseStateMachine<PlayerLocomotionContextData> _playerLocomotionStateMachine;

    private BaseEntityModifierManager _playerEntityModifierManager;
    public BaseEntityModifierManager EntityModifierManager => _playerEntityModifierManager;

    private void Awake()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerLocomotionStateMachine = new PlayerLocomotionStateMachine(null, new PlayerLocomotionContextData { PlayerAnimation = _playerAnimation, PlayerMovement = _playerMovement, PlayerTransform = transform });
        _playerEntityModifierManager = new PlayerEntityModifierManager("StatsData/Player/PlayerStatsData");
    }

    private void Update()
    {
        _playerLocomotionStateMachine.OnUpdate();
    }
}
