using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 30.0f;
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _lifetime = 3f;
    [SerializeField] private Rigidbody _rigidbody;

    private IObjectPool<Bullet> _pool;
    private Coroutine _lifetimeCoroutine;

    private void OnValidate()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        if (_rigidbody != null)
        {
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }

        _lifetimeCoroutine = StartCoroutine(LifetimeRoutine());
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + transform.forward * _speed * Time.fixedDeltaTime);
    }

    private void OnDisable()
    {
        if (_lifetimeCoroutine != null)
        {
            StopCoroutine(_lifetimeCoroutine);
            _lifetimeCoroutine = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_damage);
        }

        ReturnToPool();
    }

    public void SetPool(IObjectPool<Bullet> pool)
    {
        _pool = pool;
    }

    private IEnumerator LifetimeRoutine()
    {
        yield return new WaitForSeconds(_lifetime);
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (_pool != null)
        {
            _pool.Release(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}