using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyAggro _aggroTrigger;
    [SerializeField] private EnemyMovement _movement;

    private void OnEnable() => _aggroTrigger.OnPlayerDetected += HandlePlayerDetected;
    private void OnDisable() => _aggroTrigger.OnPlayerDetected -= HandlePlayerDetected;

    private void HandlePlayerDetected(Car car)
    {
        _movement.SetTarget(car.transform);
    }
}