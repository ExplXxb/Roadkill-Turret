using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyAggro _aggroTrigger;
    [SerializeField] private EnemyMovement _movement;
    [SerializeField] private EnemyAttack _attack;
    [SerializeField] private Health _health;
    [SerializeField] private EnemyAnimator _animator;

    [SerializeField] private float _deathAnimationDuration = 2.9667f;

    private bool _isDead = false;

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
        if (_isDead) return;

        _movement.SetTarget(car.transform);
        _animator.PlayRun();
    }

    private void HandleAttacked()
    {
        if (_isDead) return;
        Die();
    }

    private void HandleDied()
    {
        if (_isDead) return;
        Die();
    }

    private void Die()
    {
        _isDead = true;
        _animator.PlayDeath();
        _movement.Stop();
        Destroy(gameObject, _deathAnimationDuration);
    }
}