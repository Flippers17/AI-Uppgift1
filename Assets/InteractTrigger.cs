using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTrigger : MonoBehaviour
{
    public FlockInteractionType InteractType;

    [HideInInspector]
    public Transform thisTransform;

    private void OnEnable()
    {
        thisTransform = transform;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IFlockInteractable interactable))
        {
            interactable.Interact(InteractType);
        }   
    }
}
