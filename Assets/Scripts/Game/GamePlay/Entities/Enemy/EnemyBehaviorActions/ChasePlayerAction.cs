using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChasePlayer", story: "[Agent] chases Player", category: "Action/Navigation", id: "564d06bc29f4b5129e7ba42ae0917ee2")]
public partial class ChasePlayerAction : Action, IInitializable
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<EnemySensor> Sensor;
    [SerializeReference] public BlackboardVariable<BillboardText> Billboard;
    [SerializeReference] public BlackboardVariable<BaseEntityMovement> EntityMovement;
    [SerializeReference] public BlackboardVariable<EnemyAttack> EnemyAttack;
    [SerializeReference] public BlackboardVariable<NavMeshAgent> NavMeshAgent;

    private Vector3 _playerLastPosition;

    protected override Status OnStart()
    {
        if (Sensor.Value.PlayerGO == null) return Status.Failure;
        Initialize();
        EntityMovement.Value.Move(Sensor.Value.PlayerGO.transform.position);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Sensor.Value.PlayerGO == null) return Status.Failure;
        if (NavMeshAgent.Value.IsNavigationComplete()) return Status.Success;
        if (IsPlayerPositionChanged())
        {
            _playerLastPosition = Sensor.Value.PlayerGO.transform.position;
            EntityMovement.Value.Move(Sensor.Value.PlayerGO.transform.position);
        }
        return Status.Running;
    }

    protected override void OnEnd()
    {
        Billboard.Value.Reset();
    }

    public void Initialize()
    {
        Billboard.Value.SetText("I Am Chasing Player!");
        try
        {
            ((EnemyMovement)EntityMovement.Value).Stop();
        }
        catch(InvalidCastException e)
        {
            Debug.LogError(e);
        }
        _playerLastPosition = Sensor.Value.PlayerGO.transform.position;
        NavMeshAgent.Value.stoppingDistance = EnemyAttack.Value.AttackRange;
    }

    private bool IsPlayerPositionChanged() => _playerLastPosition != Sensor.Value.PlayerGO.transform.position;
}

