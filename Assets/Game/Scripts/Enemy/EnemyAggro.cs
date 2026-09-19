using System;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    public event Action<Car> OnPlayerDetected;

    private void OnTriggerEnter(Collider other)
    {
        Car car = other.GetComponentInParent<Car>();
        if (car == null) return;

        OnPlayerDetected?.Invoke(car);
    }
}
