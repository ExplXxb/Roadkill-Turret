using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private Health _health;

    public Health Health => _health;
}
