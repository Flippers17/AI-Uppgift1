using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BirdsObstacleCheckpoint : MonoBehaviour, IFlockInteractable
{
    [SerializeField]
    private BirdsObstacleCourse _course;

    [SerializeField]
    private float _resetTime = 10f;
    private float _timeUntillReset = 0;

    private bool _active = false;

    public UnityEvent OnActive;
    public UnityEvent OnInactive;


    private void OnEnable()
    {
        _course.AddCheckpoint();
    }

    public bool Interact(FlockInteractionType type, FlockManager flock)
    {
        
        _timeUntillReset = _resetTime;

        if (_active)
            return false;

        _active = true;
        _course.RemoveCheckpoint();
        OnActive?.Invoke();

        return false;
    }

    public void StopInteract(){}


    private void Update()
    {
        if(_active && !_course.courseCompleted)
        {
            _timeUntillReset -= Time.deltaTime;

            if( _timeUntillReset <= 0 )
            {
                _active = false;
                _course.AddCheckpoint();
                OnInactive?.Invoke();
            }
        }
    }
}
