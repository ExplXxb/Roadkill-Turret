using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [Tooltip("Кількість ворогів на один сегмент (префаб) дороги.")]
    [SerializeField] private int _averageEnemyCountForSegment = 5;
    [SerializeField] private float _roadWidth = 8f;
    [SerializeField] private float _minDistanceBetweenEnemies = 5f;
    [SerializeField] private float _spawnEdgeMargin = 10f;
    [SerializeField] private int _maxSpawnAttempts = 30;

    private readonly List<Vector3> _spawnedPositions = new();

    public void SpawnEnemies(float levelLength, int segmentCount)
    {
        _spawnedPositions.Clear();

        for (int i = 0; i < _averageEnemyCountForSegment * segmentCount; i++)
        {
            if (TryGetValidPosition(levelLength, out Vector3 position))
            {
                float yRotation = Random.Range(0.0f, 360.0f);
                var rotation = Quaternion.Euler(0f, yRotation, 0f);
                Instantiate(_enemyPrefab, position, rotation, transform);
                _spawnedPositions.Add(position);
            }
        }
    }

    private bool TryGetValidPosition(float levelLength, out Vector3 position)
    {
        for (int attempt = 0; attempt < _maxSpawnAttempts; attempt++)
        {
            float zPosition = Random.Range(_spawnEdgeMargin, levelLength - _spawnEdgeMargin);
            float xPosition = Random.Range(-_roadWidth / 2f, _roadWidth / 2f);
            Vector3 candidate = new Vector3(xPosition, 0f, zPosition);

            if (IsFarEnoughFromOthers(candidate))
            {
                position = candidate;
                return true;
            }
        }

        position = Vector3.zero;
        return false;
    }

    private bool IsFarEnoughFromOthers(Vector3 candidate)
    {
        foreach (Vector3 existing in _spawnedPositions)
        {
            if (Vector3.Distance(candidate, existing) < _minDistanceBetweenEnemies)
                return false;
        }

        return true;
    }
}