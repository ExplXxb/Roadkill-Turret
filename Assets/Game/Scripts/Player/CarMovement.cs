using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] GameFlowController _gameFlowController;

    [SerializeField] private float _moveSpeed = 10.0f;
    [SerializeField] private Rigidbody _rigidbody;

    private bool _isMoving = false;

    public void StartMoving() => _isMoving = true;
    public void StopMoving() => _isMoving = false;

    private void OnEnable()
    {
        _gameFlowController.OnGameStarted += StartMoving;
    }

    private void OnDisable()
    {
        _gameFlowController.OnGameStarted -= StartMoving;
    }

    private void OnValidate()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!_isMoving) return;
        _rigidbody.MovePosition(_rigidbody.position + transform.forward * _moveSpeed * Time.fixedDeltaTime);
    }

}
