using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCube : MonoBehaviour, IFlockInteractable, IKillboxable
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

    private bool _spawnNewOnDisable = true;


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

        if(_rb)
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


    public void DestroyForever()
    {
        _spawnNewOnDisable = false;
        Destroy(gameObject);
    }


    public void Kill()
    {
        _rb.velocity = Vector3.zero;
        transform.position = _startPosition;
    }

    private void OnDisable()
    {
        //if (!_spawnNewOnDisable)
        //    return;

        //InteractableCube newCube = Instantiate(this, _startPosition, Quaternion.identity);
        //newCube.enabled = true;
    }
}
