using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private static readonly int IsRunningHash = Animator.StringToHash("IsRunning");
    private static readonly int DeathHash = Animator.StringToHash("Death");

    private void OnValidate()
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();
    }

    public void PlayRun() => _animator.SetBool(IsRunningHash, true);
    public void PlayIdle() => _animator.SetBool(IsRunningHash, false);
    public void PlayDeath() => _animator.SetTrigger(DeathHash);
}