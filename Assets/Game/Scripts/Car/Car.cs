using UnityEngine;
using VContainer;

public class Car : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private CarMovement _movement;
    [SerializeField] private TurretShooting _turretShooting;
    [SerializeField] private TurretAiming _turretAiming;
    [SerializeField] private DamagedVFX _damagedVFX;

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
        _health.OnHealthChanged += HandleTakeDamage;
    }

    private void OnDisable()
    {
        _gameFlowController.OnGameStarted -= HandleGameStarted;
        _gameFlowController.OnGameEnded -= HandleGameEnded;
        _health.OnHealthChanged += HandleTakeDamage;
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

    private void HandleTakeDamage(int newHealth)
    {
        _damagedVFX.Play();
    }
}