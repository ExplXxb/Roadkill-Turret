using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Image _fillImage;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private bool _isShowOnFullHealth;
    [SerializeField] private float _hideDelayAfterDeath = 0.5f;

    private bool _hasTakenDamage;

    private void OnEnable()
    {
        bool isVisible = _isShowOnFullHealth;
        _fillImage.enabled = isVisible;
        _backgroundImage.enabled = isVisible;
        _hasTakenDamage = false;

        _health.OnHealthChanged += UpdateHealthBar;
        _health.OnDied += HandleDied;
    }

    private void OnDisable()
    {
        _health.OnHealthChanged -= UpdateHealthBar;
        _health.OnDied -= HandleDied;
    }

    private void HandleDied()
    {
        Invoke(nameof(Hide), _hideDelayAfterDeath);
    }

    private void Hide()
    {
        _fillImage.enabled = false;
        _backgroundImage.enabled = false;
    }

    private void UpdateHealthBar(int currentHealth)
    {
        if (!_hasTakenDamage)
        {
            _hasTakenDamage = true;
            _fillImage.enabled = true;
            _backgroundImage.enabled = true;
        }

        _fillImage.fillAmount = (float)currentHealth / _health.MaxHealth;
    }
}
