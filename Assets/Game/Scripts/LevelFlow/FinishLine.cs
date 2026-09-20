using System;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public event Action OnCarReachedFinish;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Car>() != null)
            OnCarReachedFinish?.Invoke();
    }
}