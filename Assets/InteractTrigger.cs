using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTrigger : MonoBehaviour
{
    public FlockInteractionType InteractType;
    [SerializeField] private BoxCollider _triggerBox;

    private FlockManager _flock;
    private IFlockInteractable _currentInteractable;
    
    [HideInInspector]
    public Transform thisTransform;

    private void OnEnable()
    {
        thisTransform = transform;
    }

    private void Start()
    {
        _flock = FlockManager.mainFlock;
    }

    public void SetSize(Vector3 size)
    {
        _triggerBox.size = size;
    }

    public void StopInteraction()
    {
        if (_currentInteractable != null)
        {
            _currentInteractable.StopInteract();
            _currentInteractable = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IFlockInteractable interactable))
        {
            if (interactable.Interact(InteractType, _flock))
            {
                _currentInteractable = interactable;
            }
        }   
    }
}
