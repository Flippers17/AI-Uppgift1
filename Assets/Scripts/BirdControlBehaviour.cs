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

    private float aimDistance = 0;

    [Space(15), SerializeField]
    private BehaviourList _blockBehaviour;
    [SerializeField]
    private BehaviourList _aimBehaviour;
    [SerializeField]
    private BehaviourList _projectileAttackBehaviour;


    private bool _freezeTarget = true;
    private Camera _cam;

    private bool _blocking = false;
    private bool _attacking = false;

    private float _attackTimer = 0;

    private void OnEnable()
    {
        //_input.OnFreezeTarget += ToggleFreezeTarget;
        _input.OnFineControl += FineControl;
        _input.OnBlock += HandleBlock;
        _input.OnAttack += DoProjectileAttack;

        _cam = Camera.main;
    }

    private void OnDisable()
    {
        //_input.OnFreezeTarget -= ToggleFreezeTarget;
        _input.OnFineControl -= FineControl;
        _input.OnBlock -= HandleBlock;
        _input.OnAttack -= DoProjectileAttack;
    }


    // Start is called before the first frame update
    void Start()
    {
        _flock = FlockManager.mainFlock;
        _flock.SetTarget(_target);
        _target.position = transform.position + Vector3.up * 5;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();

        if (_attacking)
        {
            _attackTimer -= Time.deltaTime;

            if (_attackTimer <= 0)
                StopAttack();
        }
    }


    private void UpdatePosition()
    {
        _aimPoint.position = HandleAim(out aimDistance);
        
        if (_blocking)
        {
            _target.position = transform.position;
            return;
        }
        
        if(_attacking)
            return;
        
        _target.position = transform.position + Vector3.up * 5;

        if (_freezeTarget)
            return;

        MoveTargetAtAim();
    }
    
    private void MoveTargetAtAim()
    {
        _target.position = _aimPoint.position;
    }

    private Vector3 HandleAim(out float distance)
    {
        Ray ray = _cam.ScreenPointToRay(_input.GetMousePosition());
        distance = 20;
        
        if (Physics.Raycast(ray, out RaycastHit hit, 100, _targetAimMask))
        {
            distance = hit.distance;
            return hit.point; //+ 3 * Vector3.up;
        }
        else
            return _cam.transform.position + ray.direction * 20;
    }
    
    
    private void HandleBlock(bool state)
    {
        if (state)
        {
            _blocking = true;
            _attacking = false;
            _flock.SetSteeringBehaviour(_blockBehaviour);
        }
        else if(_blocking)
        {
            _flock.SetSteeringBehaviour(_aimBehaviour);
            _blocking = false;
            _target.position = transform.position + Vector3.up * 5;
        }
    }

    
    private void ToggleFreezeTarget()
    {
        _freezeTarget = !_freezeTarget;
    }

    private void FineControl(bool value)
    {
        _freezeTarget = !value;
    }


    private void DoProjectileAttack()
    {
        if(_attacking)
            return;
        
        _attacking = true;
        _blocking = false;
        MoveTargetAtAim();
        float distance = Vector3.Distance(_aimPoint.position, _flock.GetAverageFlockPosition());
        _attackTimer = (distance / _projectileAttackBehaviour.maxSpeed) + .2f;
        _flock.SetSteeringBehaviour(_projectileAttackBehaviour);
    }

    private void StopAttack()
    {
        _attacking = false;
        _flock.SetSteeringBehaviour(_aimBehaviour);
    }
}
