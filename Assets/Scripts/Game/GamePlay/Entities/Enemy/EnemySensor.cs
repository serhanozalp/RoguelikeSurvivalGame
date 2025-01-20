using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySensor : MonoBehaviour, IInitializable
{
    [SerializeField] private SphereCollider _sensorCollider;

    private readonly List<GameObject> _detectedObjects = new List<GameObject>();
    private GameObject _playerGO;
    public GameObject PlayerGO { get => _playerGO; }

    private BaseStatsData _runtimeStatsData;

    [Inject]
    private void ZenjectConstructor([Inject(Id ="Runtime")] BaseStatsData runtimeStatsData)
    {
        _runtimeStatsData = runtimeStatsData;
    }

    private void Awake()
    {
        Debug.Assert(_sensorCollider != null, $"{gameObject.name} is missing Sensor Collider!");
        Initialize();
    }

    public void Initialize()
    {
        try
        {
            _sensorCollider.radius = ((EnemyStatsData)_runtimeStatsData).SensorRange.Value;
        }
        catch (InvalidCastException e)
        {
            Debug.LogError(e.Message);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") _playerGO = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") _playerGO = null;
    }
}
