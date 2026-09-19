using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyAggro _aggroTrigger;
    [SerializeField] private EnemyMovement _movement;
    [SerializeField] private EnemyAttack _attack;
    [SerializeField] private Health _health;

    private void OnEnable()
    {
        _aggroTrigger.OnPlayerDetected += HandlePlayerDetected;
        _attack.OnAttacked += HandleAttacked;
        _health.OnDied += HandleDied;
    }

    private void OnDisable()
    {
        _aggroTrigger.OnPlayerDetected -= HandlePlayerDetected;
        _attack.OnAttacked -= HandleAttacked;
        _health.OnDied -= HandleDied;
    }

    private void HandlePlayerDetected(Car car)
    {
        _movement.SetTarget(car.transform);
    }

    private void HandleAttacked() => Die();
    private void HandleDied() => Die();

    private void Die()
    {
        _movement.Stop();
        Destroy(gameObject);
    }
}