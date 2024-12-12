using UnityEngine;

[RequireComponent(typeof(PlayerAnimation), typeof(PlayerMovement))]
public class Player : MonoBehaviour , ICombatEntity
{
    private PlayerAnimation _playerAnimation;
    private PlayerMovement _playerMovement;

    private PlayerLocomotionStateMachine _playerLocomotionStateMachine;

    private BaseStatsManager _playerStatsManager;

    public BaseStatsManager CombatEntityStatsManager => _playerStatsManager;

    private void Awake()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerLocomotionStateMachine = new PlayerLocomotionStateMachine(null, new LocomotionContextData { PlayerAnimation = _playerAnimation, PlayerMovement = _playerMovement, PlayerTransform = transform });
        _playerStatsManager = new PlayerStatsManager("StatsData/Player/PlayerStatsData");
    }

    private void Update()
    {
        _playerLocomotionStateMachine.OnUpdate();
    }

    private void LateUpdate()
    {
        _playerLocomotionStateMachine.OnLateUpdate();
    }
}
