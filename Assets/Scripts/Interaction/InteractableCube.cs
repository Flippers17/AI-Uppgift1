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

    [SerializeField]
    private LayerMask _pickedUpCollision;

    [SerializeField]
    private float _dropDistance = 15f;
    [SerializeField]
    private Transform _dropPositionEffect;


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
        if(_dropPositionEffect)
            _dropPositionEffect.gameObject.SetActive(true);

        //Debug.Log("Started interact");
        return true;
    }

    public void StopInteract()
    {
        _beingInteractedWith = false;
        _rb.isKinematic = false;


        if (_dropPositionEffect)
            _dropPositionEffect.gameObject.SetActive(false);
        //Debug.Log("Stopped interact");
    }

    private void Update()
    {
        if (_beingInteractedWith)
        {
            Vector3 newPos = _flock.averagePos;
            if(!Physics.Raycast(transform.position, newPos - transform.position, (newPos-transform.position).magnitude, _pickedUpCollision))
            {
                transform.position = newPos;
            }
            else if(Vector3.SqrMagnitude(transform.position - newPos) > _dropDistance * _dropDistance)
            {
                StopInteract();
            }

            if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 100, _pickedUpCollision))
            {
                if(_dropPositionEffect)
                    _dropPositionEffect.position = hit.point;
            }
        }
    }

    private void OnDisable()
    {
        InteractableCube newCube = Instantiate(this, _startPosition, Quaternion.identity);
        newCube.enabled = true;
    }
}
