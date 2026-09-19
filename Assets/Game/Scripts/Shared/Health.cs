using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth = 20;

    public event Action OnDied;
    public event Action<int> OnHealthChanged;

    private int _currentHealth;

    private void Awake() => _currentHealth = _maxHealth;

    public void TakeDamage(int amount)
    {
        if (_currentHealth <= 0) return;

        _currentHealth = Mathf.Max(0, _currentHealth - amount);
        OnHealthChanged?.Invoke(_currentHealth);

        if (_currentHealth <= 0)
            OnDied?.Invoke();
    }
}