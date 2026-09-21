using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class TurretShooting : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _fireRate = 0.5f;
    [Header("Flashlight Effect")]
    [SerializeField] private Light _light;
    [SerializeField] private float _blinkingTime = 0.05f;

    private bool _isShooting = false;
    private float _cooldownTimer;
    private IObjectPool<Bullet> _bulletPool;
    private Transform _poolContainer;
    private Coroutine _lightBlinkingRoutine = null;

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

        if (_light != null)
        {
            _light.enabled = false;
        }
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
        Bullet bullet = _bulletPool.Get();

        bullet.ActivateTrail();

        if (_light != null)
        {
            if (_lightBlinkingRoutine != null)
            {
                StopCoroutine(_lightBlinkingRoutine);
            }
            _lightBlinkingRoutine = StartCoroutine(LightBlinkingRoutine());
        }
    }

    private IEnumerator LightBlinkingRoutine()
    {
        _light.enabled = true;

        yield return new WaitForSeconds(_blinkingTime);

        _light.enabled = false;
        _lightBlinkingRoutine = null;
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