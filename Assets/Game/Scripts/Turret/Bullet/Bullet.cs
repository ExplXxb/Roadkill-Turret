using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 30.0f;
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _lifetime = 3f;
    [SerializeField] private Rigidbody _rigidbody;

    private void OnValidate()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, _lifetime);
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + transform.forward * _speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}