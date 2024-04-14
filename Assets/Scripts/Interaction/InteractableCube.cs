using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCube : MonoBehaviour, IFlockInteractable
{
    [SerializeField]
    private FlockInteractionType _interactionType;

    private bool _beingInteractedWith;

    [SerializeField]
    private Rigidbody _rb;

    private FlockManager _flock;

    private Vector3 _startPosition;

    private void OnEnable()
    {
        _startPosition = transform.position;
    }

    public bool Interact(FlockInteractionType type, FlockManager flock)
    {   
        if(type != _interactionType)
            return false;

        _beingInteractedWith = true;
        _flock = flock;
        _rb.isKinematic = true;
        return true;
    }

    public void StopInteract()
    {
        _beingInteractedWith = false;
        _rb.isKinematic = false;
    }

    private void Update()
    {
        if (_beingInteractedWith)
            transform.position = _flock.averagePos;
    }

    private void OnDisable()
    {
        InteractableCube newCube = Instantiate(this, _startPosition, Quaternion.identity);
        newCube.enabled = true;
    }
}
