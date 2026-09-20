using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameFlowController : MonoBehaviour
{
    [SerializeField] private InputActionReference _tapAction;
    [SerializeField] private LevelController _levelController;

    public event Action OnGameStarted;
    public event Action<bool> OnGameEnded;

    private void Start()
    {
        RunGameLoop(this.GetCancellationTokenOnDestroy()).Forget();
    }

    private async UniTaskVoid RunGameLoop(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            await WaitForTapAsync(token);
            OnGameStarted?.Invoke();

            bool won = await _levelController.RunLevelAsync(token);
            OnGameEnded?.Invoke(won);

            await WaitForTapAsync(token);
        }
    }

    private async UniTask WaitForTapAsync(CancellationToken token)
    {
        _tapAction.action.Enable();
        await UniTask.WaitUntil(() => _tapAction.action.WasPerformedThisFrame(), cancellationToken: token);
    }
}