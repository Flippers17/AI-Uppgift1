using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;

public class BirdControlBehaviour : MonoBehaviour
{
    private FlockManager _flock;

    [SerializeField]
    private PlayerInputHandler _input;

    [SerializeField]
    private LayerMask _targetAimMask;

    [SerializeField]
    private Transform _target;
    [SerializeField]
    private Transform _aimPoint;
    [HideInInspector]
    public Transform thisTransform;
    [SerializeField]
    private InteractTrigger _interacter;


    private BirdControlState _currentControlState;

    [Space(15), Header("States"), SerializeField]
    private BirdIdleControlState _idle;

    [SerializeField] private BirdTargetControlBehaviour _targetingState;
    [SerializeField] private BirdBlockControlBehaviour _blockState;
    [SerializeField] private BirdAttackState _attackState;


    private Camera _cam;


    private void OnEnable()
    {
        _input.OnFineControl += FineControl;
        _input.OnBlock += HandleBlock;
        _input.OnAttack += DoProjectileAttack;

        _cam = Camera.main;
        thisTransform = transform;
    }

    private void OnDisable()
    {
        _input.OnFineControl -= FineControl;
        _input.OnBlock -= HandleBlock;
        _input.OnAttack -= DoProjectileAttack;
    }


    // Start is called before the first frame update
    void Start()
    {
        _flock = FlockManager.mainFlock;
        _flock.SetTarget(_target);

        _currentControlState = _idle;
        _currentControlState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        _aimPoint.position = HandleAim();
        _currentControlState.UpdateState(this, Time.deltaTime);
        _interacter.thisTransform.position = GetAverageFlockPos();
    }


    public void MoveTargetAtAim()
    {
        _target.position = _aimPoint.position;
    }

    private Vector3 HandleAim()
    {
        Ray ray = _cam.ScreenPointToRay(_input.GetMousePosition());
        
        if (Physics.Raycast(ray, out RaycastHit hit, 100, _targetAimMask))
        {
            return hit.point;
        }
        else
            return _cam.transform.position + ray.direction * 20;
    }
    
    
    private void HandleBlock(bool state)
    {

        if (state)
        {
            TransitionState(_blockState);
        }
        else if(_currentControlState == _blockState)
        {
            TransitionState(_idle);
        }
    }


    private void FineControl(bool value)
    {
        if(value)
        {
            TransitionState(_targetingState);
        }
        else if( _currentControlState == _targetingState)
        {
            TransitionState(_idle);
        }
    }


    private void DoProjectileAttack()
    {
        if(_currentControlState == _attackState)
            return;

        Vector3 averagePos = _flock.averagePos;
        Debug.DrawLine(averagePos, averagePos + Vector3.up, Color.green, 5f);
        if(Vector3.SqrMagnitude(averagePos - transform.position) > 64f)
            return;
        
        TransitionState(_attackState);
    }


    public Vector3 GetAverageFlockPos()
    {
        return _flock.averagePos;
    }

    public Vector3 GetAimPointPos()
    {
        return _aimPoint.position;
    }

    public void TransitionToIdle()
    {
        TransitionState(_idle);
    }

    public void TransitionState(BirdControlState state)
    {
        _currentControlState.ExitState(this);
        _currentControlState = state;
        _currentControlState.EnterState(this);
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        _target.position = targetPosition;
    }

    public void SetInteractionType(FlockInteractionType type)
    {
        _interacter.InteractType = type;
    }

    

    public void SetSteeringBehaviour(BehaviourList behaviourList)
    {
        _flock.SetSteeringBehaviour(behaviourList);
    }
}
