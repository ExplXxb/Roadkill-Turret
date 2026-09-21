using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyAggro _aggroTrigger;
    [SerializeField] private EnemyMovement _movement;
    [SerializeField] private EnemyAttack _attack;
    [SerializeField] private Health _health;
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] private DamagedVFX _damagedVFX;

    [SerializeField] private float _deathAnimationDuration = 2.9667f;

    private Collider _hitboxCollider; // одночасно й для атаки, й для отримання шкоди
    private bool _isDead = false;

    private void Awake()
    {
        _hitboxCollider = _attack.GetComponent<Collider>();
    }

    private void OnEnable()
    {
        _hitboxCollider.enabled = true; // на випадок повторного використання через object pooling у майбутньому

        _aggroTrigger.OnPlayerDetected += HandlePlayerDetected;
        _attack.OnAttacked += HandleAttacked;
        _health.OnHealthChanged += HandleTakeDamage;
        _health.OnDied += HandleDied;
    }

    private void OnDisable()
    {
        _aggroTrigger.OnPlayerDetected -= HandlePlayerDetected;
        _attack.OnAttacked -= HandleAttacked;
        _health.OnHealthChanged += HandleTakeDamage;
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

    private void HandleTakeDamage(int newHealth)
    {
        _damagedVFX.Play();
    }

    private void HandleDied()
    {
        if (_isDead) return;
        Die();
    }

    private void Die()
    {
        _isDead = true;
        _hitboxCollider.enabled = false;
        _animator.PlayDeath();
        _movement.Stop();
        Destroy(gameObject, _deathAnimationDuration);
    }
}