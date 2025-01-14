using UnityEngine;
using Zenject;

public class BaseEntityMovement : MonoBehaviour
{
    protected Vector3 _moveDirection;
    public Vector3 MoveDirection { get { return _moveDirection; } }

    protected float _moveSpeed;
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    protected BaseStatsData _entityStatsData;

    [Inject]
    private void ZenjectConstructor([Inject(Id = "Runtime")] BaseStatsData statsData)
    {
        _entityStatsData = statsData;
    }
}
