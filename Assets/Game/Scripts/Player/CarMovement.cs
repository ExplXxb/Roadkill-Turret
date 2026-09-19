using UnityEngine;
using VContainer;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10.0f;
    [SerializeField] private Rigidbody _rigidbody;

    GameFlowController _gameFlowController;
    private bool _isMoving = false;

    [Inject]
    public void Construct(GameFlowController gameFlowController)
    {
        _gameFlowController = gameFlowController;
    }
    private void OnValidate()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _gameFlowController.OnGameStarted += StartMoving;
    }

    private void OnDisable()
    {
        _gameFlowController.OnGameStarted -= StartMoving;
    }

    private void FixedUpdate()
    {
        if (!_isMoving) return;
        _rigidbody.MovePosition(_rigidbody.position + transform.forward * _moveSpeed * Time.fixedDeltaTime);
    }

    public void StartMoving() => _isMoving = true;
    public void StopMoving() => _isMoving = false;
}
