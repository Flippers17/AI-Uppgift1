using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlockInteraction : MonoBehaviour, IFlockInteractable
{
    public FlockInteractionType interactionType;
    public UnityEvent OnInteractedWith;
    public UnityEvent OnStopInteracted;
    
    
    public bool Interact(FlockInteractionType type, FlockManager flock)
    {
        if (type == interactionType)
        {
            OnInteractedWith?.Invoke();
            return true;
        }

        return false;
    }

    public void StopInteract()
    {
        OnStopInteracted?.Invoke();
    }
}

public enum FlockInteractionType
{
    None,
    Attack,
    Interact
}


public interface IFlockInteractable
{
    public bool Interact(FlockInteractionType type, FlockManager flock);

    public void StopInteract();
}
