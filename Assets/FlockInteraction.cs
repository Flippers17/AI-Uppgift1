using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlockInteraction : MonoBehaviour, FlockInteractable
{
    public FlockInteractionType interactionType;
    public UnityEvent OnInteractedWith;
    
    
    public void Interact(FlockInteractionType type)
    {
        if(type == interactionType)
            OnInteractedWith?.Invoke();
    }
}

public enum FlockInteractionType
{
    Attack,
    Interact
}


public interface FlockInteractable
{
    public void Interact(FlockInteractionType type){}
    
    
}
