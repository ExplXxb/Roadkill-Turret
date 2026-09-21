using UnityEngine;
using VContainer;

public class TutorialHintUI : MonoBehaviour
{
    [SerializeField] private GameObject _hintPanel;

    private GameFlowController _gameFlowController;

    [Inject]
    public void Construct(GameFlowController gameFlowController)
    {
        _gameFlowController = gameFlowController;
    }

    private void OnEnable() => _gameFlowController.OnGameStarted += Hide;
    private void OnDisable() => _gameFlowController.OnGameStarted -= Hide;

    private void Hide() => _hintPanel.SetActive(false);
}