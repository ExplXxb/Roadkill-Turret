using UnityEngine;

public class DamagedVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem _damagedPrefabVFX;
    [SerializeField] private Transform _spawnPointTransform;

    public void Play()
    {
        var vfx = Instantiate(_damagedPrefabVFX, _spawnPointTransform.position, Quaternion.identity);
    }
}