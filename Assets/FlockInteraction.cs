using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlockInteraction : MonoBehaviour, IFlockInteractable
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
    None,
    Attack,
    Interact
}


public interface IFlockInteractable
{
    public void Interact(FlockInteractionType type){}
    
    
}
