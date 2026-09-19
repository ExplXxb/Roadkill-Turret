using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

public class TurretAiming : MonoBehaviour
{
    [SerializeField] private InputActionReference _pointerPositionAction;
    [SerializeField] private Transform _turretPivot;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _rotationSpeed = 10f;

    private bool _canAim;

    private GameFlowController _gameFlowController;

    [Inject]
    public void Construct(GameFlowController gameFlowController)
    {
        _gameFlowController = gameFlowController;
    }

    private void OnEnable()
    {
        _pointerPositionAction.action.Enable();
        _gameFlowController.OnGameStarted += StartAiming;
    }

    private void OnDisable()
    {
        _pointerPositionAction.action.Disable();
        _gameFlowController.OnGameStarted -= StartAiming;
    }

    private void Update()
    {
        if (!_canAim) return;

        Vector2 screenPosition = _pointerPositionAction.action.ReadValue<Vector2>();
        RotateTurretTowards(screenPosition);
    }

    private void StartAiming() => _canAim = true;
    private void StopAiming() => _canAim = false;

    private void RotateTurretTowards(Vector2 screenPosition)
    {
        Vector3? aimPoint = GetAimPointOnPlane(screenPosition, planeHeight: _turretPivot.position.y);

        if (!aimPoint.HasValue) return;

        Vector3 direction = aimPoint.Value - _turretPivot.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _turretPivot.rotation = Quaternion.Slerp(
                _turretPivot.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }

    private Vector3? GetAimPointOnPlane(Vector2 screenPosition, float planeHeight)
    {
        Ray ray = _camera.ScreenPointToRay(screenPosition);
        Plane plane = new Plane(Vector3.up, new Vector3(0, planeHeight, 0));

        if (plane.Raycast(ray, out float distance))
            return ray.GetPoint(distance);

        return null;
    }
}