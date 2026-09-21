using UnityEngine;
using UnityEngine.Pool;

public class TurretShooting : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _fireRate = 0.5f;

    private bool _isShooting = false;
    private float _cooldownTimer;
    private IObjectPool<Bullet> _bulletPool;
    private Transform _poolContainer;

    private void Awake()
    {
        _poolContainer = new GameObject($"Pool_{gameObject.name}_Bullets").transform;

        _bulletPool = new ObjectPool<Bullet>(
            createFunc: CreateBullet,
            actionOnGet: OnGetBullet,
            actionOnRelease: OnReleaseBullet,
            actionOnDestroy: OnDestroyBullet,
            collectionCheck: true,
            defaultCapacity: 20,
            maxSize: 100
        );
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

    public void StartShooting() => _isShooting = true;
    public void StopShooting() => _isShooting = false;

    private void Shoot()
    {
        _bulletPool.Get();
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(_bulletPrefab, _poolContainer);
        bullet.SetPool(_bulletPool);
        return bullet;
    }

    private void OnGetBullet(Bullet bullet)
    {
        bullet.transform.SetPositionAndRotation(_firePoint.position, _firePoint.rotation);
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}