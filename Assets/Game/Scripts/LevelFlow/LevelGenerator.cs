using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _groundSegmentPrefab;
    [SerializeField] private GameObject _finishLinePrefab;
    [SerializeField] private float _segmentLength = 20f;
    [SerializeField] private int _segmentCount = 10;
    [Tooltip("Додаткові сегменти дороги, що потрібно додати, щоб візуально завершити рівень.")]
    [SerializeField] private int _extraSegmentsCount = 3;
    [SerializeField] private LevelController _levelController;
    [SerializeField] private EnemySpawner _enemySpawner;

    private void Awake()
    {
        float realLevelLength = _segmentCount * _segmentLength - _segmentLength / 2;

        GenerateRoadSegments(_segmentCount);
        SpawnFinishLine(realLevelLength);
        GenerateRoadSegments(_extraSegmentsCount, offset: _segmentCount * _segmentLength);

        _enemySpawner.SpawnEnemies(realLevelLength, _segmentCount);
    }

    private void GenerateRoadSegments(int segmentCount, float offset = 0.0f)
    {
        for (int i = 0; i < segmentCount; i++)
        {
            Vector3 position = new Vector3(0, 0, i * _segmentLength + offset);
            Instantiate(_groundSegmentPrefab, position, Quaternion.identity, transform);
        }
    }

    private void SpawnFinishLine(float levelLength)
    {
        GameObject finishObj = Instantiate(_finishLinePrefab, new Vector3(0, 0, levelLength), Quaternion.identity, transform);
        _levelController.SetFinishLine(finishObj.GetComponent<FinishLine>());
    }
}