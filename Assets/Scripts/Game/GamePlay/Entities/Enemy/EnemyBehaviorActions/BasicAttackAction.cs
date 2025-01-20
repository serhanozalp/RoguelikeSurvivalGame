using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BasicAttack", story: "[Agent] attacks Player", category: "Action", id: "c9330b38f9fb2889ae021c4ad48e1590")]
public partial class BasicAttackAction : Action, IInitializable
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<EnemyAttack> EnemyAttack;
    [SerializeReference] public BlackboardVariable<BillboardText> Billboard;
    [SerializeReference] public BlackboardVariable<EnemySensor> Sensor;

    protected override Status OnStart()
    {
        if (Sensor.Value.PlayerGO == null) return Status.Failure;
        if(!IsPlayerInAttackRange()) return Status.Failure;
        Initialize();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (!IsPlayerInAttackRange()) return Status.Failure;
        EnemyAttack.Value.Attack();
        return Status.Running;
    }

    protected override void OnEnd()
    {
        Billboard.Value.Reset();
    }

    public void Initialize()
    {
        Billboard.Value.SetText("I Am Attacking Player!");
    }

    private bool IsPlayerInAttackRange() => (Vector3.Distance(Sensor.Value.PlayerGO.transform.position, Agent.Value.transform.position) <= EnemyAttack.Value.AttackRange);
}

