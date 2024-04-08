using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BirdControlBehaviour : MonoBehaviour
{
    private FlockManager _flock;

    [SerializeField]
    private PlayerInputHandler _input;

    [SerializeField]
    private LayerMask _targetAimMask;

    [SerializeField]
    private Transform _target;

    [Space(15), SerializeField]
    private BehaviourList _blockBehaviour;
    [Space(15), SerializeField]
    private BehaviourList _aimBehaviour;


    private bool _freezeTarget = false;
    private Camera _cam;

    private bool _blocking = false;


    private void OnEnable()
    {
        _input.OnFreezeTarget += ToggleFreezeTarget;
        _input.OnBlock += HandleBlock;

        _cam = Camera.main;
    }

    private void OnDisable()
    {
        _input.OnFreezeTarget -= ToggleFreezeTarget;
        _input.OnBlock -= HandleBlock;
    }


    // Start is called before the first frame update
    void Start()
    {
        _flock = FlockManager.mainFlock;
        _flock.SetTarget(_target);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }


    private void UpdatePosition()
    {

        if (_blocking)
        {
            _target.position = transform.position;
            return;
        }

        if (_freezeTarget)
            return;

        MoveTargetAtAim();
    }

    private void MoveTargetAtAim()
    {
        Ray ray = _cam.ScreenPointToRay(_input.GetMousePosition());

        if (Physics.Raycast(ray, out RaycastHit hit, 100, _targetAimMask))
        {
            _target.position = hit.point; //+ 3 * Vector3.up;
        }
    }
    
    private void HandleBlock(bool state)
    {
        if (state)
        {
            _blocking = true;
            _flock.SetSteeringBehaviour(_blockBehaviour);
        }
        else
        {
            _flock.SetSteeringBehaviour(_aimBehaviour);
            _blocking = false;
            MoveTargetAtAim();
        }
    }

    
    private void ToggleFreezeTarget()
    {
        _freezeTarget = !_freezeTarget;
    }
}
