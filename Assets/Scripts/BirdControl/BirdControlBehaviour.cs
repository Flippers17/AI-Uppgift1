using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;
using static UnityEngine.Rendering.DebugUI;

public class BirdControlBehaviour : MonoBehaviour
{
    private FlockManager _flock;

    [SerializeField]
    private PlayerInputHandler _input;
    public Health health;
    public Animator _anim;

    [Space(15), SerializeField]
    private LayerMask _targetAimMask;

    [SerializeField]
    private Transform _target;
    [SerializeField]
    private Transform _aimPoint;
    [HideInInspector]
    public Transform thisTransform;
    [SerializeField]
    private InteractTrigger _interacter;

    [SerializeField] private float _aimSurfaceDistance = 3f;
    private Vector3 _aimSurfaceNormal;
    

    
    
    [Space(15), Header("States")]
    public BirdIdleControlState _idle;
    public BirdTargetControlBehaviour _targetingState;
    public BirdBlockControlBehaviour _blockState;
    public BirdAttackState _attackState;

    private BirdControlState _currentControlState;

    
    
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



    void Start()
    {
        _flock = FlockManager.mainFlock;
        _flock.SetTarget(_target);

        _currentControlState = _idle;
        _currentControlState.EnterState(this);
    }


    void Update()
    {
        _aimPoint.position = HandleAim();
        _currentControlState.UpdateState(this, Time.deltaTime);
        _interacter.thisTransform.position = GetAverageFlockPos();
    }


    public void MoveTargetAtAim()
    {
        _target.position = _aimPoint.position + (_aimSurfaceNormal * _aimSurfaceDistance);
    }

    private Vector3 HandleAim()
    {
        Ray ray = _cam.ScreenPointToRay(_input.GetMousePosition());
        
        if (Physics.Raycast(ray, out RaycastHit hit, 100, _targetAimMask))
        {
            _aimSurfaceNormal = hit.normal;
            return hit.point;
        }
        else
        {
            _aimSurfaceNormal = Vector3.zero;
            return _cam.transform.position + ray.direction * 20;
        }
    }

    public void TransitionState(BirdControlState state)
    {
        _currentControlState.ExitState(this);
        _currentControlState = state;
        _currentControlState.EnterState(this);
    }



    #region ControlEvents

    private void HandleBlock(bool state)
    {
        _currentControlState.OnControlEvent(this, ControlEvent.Block, state);
    }


    private void FineControl(bool value)
    {
        _currentControlState.OnControlEvent(this, ControlEvent.FineControl, value);
    }


    private void DoProjectileAttack()
    {
        _currentControlState.OnControlEvent(this, ControlEvent.Attack, false);
    }


    #endregion


    #region GetAndSet

    public Vector3 GetAverageFlockPos()
    {
        return _flock.averagePos;
    }

    public Vector3 GetAimPointPos()
    {
        return _aimPoint.position;
    }
    
    public Vector3 GetTargetPosition()
    {
        return _target.position;
    }
    
    public void SetTargetPosition(Vector3 targetPosition)
    {
        _target.position = targetPosition;
    }

    public void SetInteraction(FlockInteractionType type, Vector3 size)
    {
        _interacter.InteractType = type;
        _interacter.SetSize(size);
    }


    public void SetSteeringBehaviour(BehaviourList behaviourList)
    {
        _flock.SetSteeringBehaviour(behaviourList);
    }

    #endregion


    public void StopCurrentInteraction()
    {
        _interacter.StopInteraction();
    }
}
