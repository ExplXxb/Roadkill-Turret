using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _fireRate = 0.5f;

    private bool _isShooting = false;
    private float _cooldownTimer;

    private void Update()
    {
        _cooldownTimer -= Time.deltaTime;

        if (_cooldownTimer <= 0f && _isShooting)
        {
            Shoot();
            _cooldownTimer = _fireRate;
        }
    }

    public void StartShooting() => _isShooting = true;
    public void StopShooting() => _isShooting = false;

    private void Shoot()
    {
        Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
    }
}