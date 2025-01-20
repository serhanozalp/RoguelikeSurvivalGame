using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : BaseEntityMovement
{
    private NavMeshAgent _navMeshAgent;
    public override float MoveSpeed { get => _moveSpeed; set { _moveSpeed = value; _navMeshAgent.speed = _moveSpeed; }}

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected override void Update()
    {
        base.Update();
        SetMoveDirection();
    }

    public override void Move(Vector3 destination) => _navMeshAgent.SetDestination(destination);

    public void Stop() => _navMeshAgent.ResetPath();

    private void SetMoveDirection() => _moveDirection = _navMeshAgent.velocity.normalized;

}
