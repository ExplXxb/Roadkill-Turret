using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int _damage = 10;

    public event Action OnAttacked;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out IDamageable damageable)) return;
        if (other.GetComponentInParent<Car>() == null) return;

        damageable.TakeDamage(_damage);
        OnAttacked?.Invoke();
    }
}