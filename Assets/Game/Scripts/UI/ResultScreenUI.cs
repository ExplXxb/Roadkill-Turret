using UnityEngine;
using VContainer;

public class ResultScreenUI : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;

    private GameFlowController _gameFlowController;

    [Inject]
    public void Construct(GameFlowController gameFlowController)
    {
        _gameFlowController = gameFlowController;
    }

    private void OnEnable()
    {
        _gameFlowController.OnGameStarted += Hide;
        _gameFlowController.OnGameEnded += Show;
    }

    private void OnDisable()
    {
        _gameFlowController.OnGameStarted -= Hide;
        _gameFlowController.OnGameEnded -= Show;
    }

    private void Show(bool won)
    {
        _winPanel.SetActive(won);
        _losePanel.SetActive(!won);
    }

    private void Hide()
    {
        _winPanel.SetActive(false);
        _losePanel.SetActive(false);
    }
}