using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent OnTriggered;

    public LayerMask triggerMask;

    private void OnTriggerEnter(Collider other)
    {
        if(((1 << other.gameObject.layer) & triggerMask) != 0)
        {
            OnTriggered?.Invoke();
        }
    }
}
