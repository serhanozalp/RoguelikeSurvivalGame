using System;
using UnityEngine;
using Zenject;

public class EnemyAttack : MonoBehaviour, IInitializable
{
    private BaseStatsData _runtimeStatsData;
    private float _attackRange;
    public float AttackRange { get => _attackRange; }

    private CountdownTimer _attackRateCountdownTimer;
    private bool _canAttack;


    [Inject]
    private void ZenjectConstructor([Inject(Id ="Runtime")] BaseStatsData runtimeStatsData)
    {
        _runtimeStatsData = runtimeStatsData;
    }

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        _attackRateCountdownTimer = new CountdownTimer(_runtimeStatsData.AttackRate.Value);
        _attackRateCountdownTimer.TimerStoped += ResetAttackCooldown;
        _canAttack = true;
        try
        {
            _attackRange = ((EnemyStatsData)_runtimeStatsData).AttackRange.Value;
        }
        catch (InvalidCastException e)
        {
            Debug.LogError(e.Message);
        }
    }

    public void Attack()
    {
        if (_canAttack)
        {
            _canAttack = false;
            _attackRateCountdownTimer.Start();
            Debug.Log("Attack!");
        }
    }

    private void ResetAttackCooldown() => _canAttack = true;

    private void OnDestroy()
    {
        _attackRateCountdownTimer.TimerStoped -= ResetAttackCooldown;
    }
}
