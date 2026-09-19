using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private Transform _target;
    private bool _isChasing;

    public void SetTarget(Transform target)
    {
        _target = target;
        _isChasing = true;
    }

    public void Stop() => _isChasing = false;

    private void FixedUpdate()
    {
        if (!_isChasing || _target == null) return;

        Vector3 direction = (_target.position - transform.position);
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.01f)
        {
            transform.position += direction.normalized * _moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(direction.normalized);
        }
    }
}