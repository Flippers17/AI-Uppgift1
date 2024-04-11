using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetBehaviour : MonoBehaviour
{

    public UnityEvent<int> OnTakeDamage;

    public void TakeDamage(int damage)
    {
        OnTakeDamage?.Invoke(damage);
    }
}
