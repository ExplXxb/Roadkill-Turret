using UnityEngine;
using VContainer;

public class Car : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private CarMovement _movement;
    [SerializeField] private TurretShooting _turretShooting;
    [SerializeField] private TurretAiming _turretAiming;

    private GameFlowController _gameFlowController;

    [Inject]
    public void Construct(GameFlowController gameFlowController)
    {
        _gameFlowController = gameFlowController;
    }

    public Health Health => _health;

    private void OnEnable()
    {
        _gameFlowController.OnGameStarted += HandleGameStarted;
        _gameFlowController.OnGameEnded += HandleGameEnded;
    }

    private void OnDisable()
    {
        _gameFlowController.OnGameStarted -= HandleGameStarted;
        _gameFlowController.OnGameEnded -= HandleGameEnded;
    }

    private void HandleGameStarted()
    {
        _movement.StartMoving();
        _turretShooting.StartShooting();
        _turretAiming.StartAiming();
    }

    private void HandleGameEnded(bool won)
    {
        _movement.StopMoving();
        _turretShooting.StopShooting();
        _turretAiming.StopAiming();
    }
}