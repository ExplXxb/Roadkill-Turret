using UnityEngine;
using VContainer;

public class TurretShooting : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _fireRate = 0.5f;

    private bool _isShooting = false;
    private float _cooldownTimer;

    private GameFlowController _gameFlowController;

    [Inject]
    public void Construct(GameFlowController gameFlowController)
    {
        _gameFlowController = gameFlowController;
    }

    private void OnEnable()
    {
        _gameFlowController.OnGameStarted += StartShooting;
    }

    private void OnDisable()
    {
        _gameFlowController.OnGameStarted -= StartShooting;
    }

    private void Update()
    {
        _cooldownTimer -= Time.deltaTime;

        if (_cooldownTimer <= 0f && _isShooting)
        {
            Shoot();
            _cooldownTimer = _fireRate;
        }
    }

    private void StartShooting() => _isShooting = true;
    private void StopShooting() => _isShooting = false;

    private void Shoot()
    {
        Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
    }
}