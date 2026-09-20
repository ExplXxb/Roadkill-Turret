using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using VContainer;

public class LevelController : MonoBehaviour
{
    private FinishLine _finishLine;
    private Health _carHealth;

    [Inject]
    public void Construct(Car car)
    {
        _carHealth = car.Health;
    }

    public void SetFinishLine(FinishLine finishLine)
    {
        _finishLine = finishLine;
    }

    public async UniTask<bool> RunLevelAsync(CancellationToken token)
    {
        var loseTask = WaitForCarDeathAsync(token);
        var winTask = WaitForFinishAsync(token);

        int winnerIndex = await UniTask.WhenAny(loseTask, winTask);
        return winnerIndex == 1;
    }

    private async UniTask WaitForCarDeathAsync(CancellationToken token)
    {
        var tcs = new UniTaskCompletionSource();
        void OnDied() => tcs.TrySetResult();

        _carHealth.OnDied += OnDied;
        try
        {
            await tcs.Task.AttachExternalCancellation(token);
        }
        finally
        {
            _carHealth.OnDied -= OnDied;
        }
    }

    private async UniTask WaitForFinishAsync(CancellationToken token)
    {
        var tcs = new UniTaskCompletionSource();
        void OnFinish() => tcs.TrySetResult();

        _finishLine.OnCarReachedFinish += OnFinish;
        try
        {
            await tcs.Task.AttachExternalCancellation(token);
        }
        finally
        {
            _finishLine.OnCarReachedFinish -= OnFinish;
        }
    }
}
