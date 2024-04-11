using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    public Animator _anim;


    private EnemyState _currentState;

    [SerializeField]
    private float _attackDistance = 50f;
    [SerializeField]
    private float _bulletSpread = 1f;
    [SerializeField]
    private LayerMask _hitMask;


    public EnemyIdleState idleState;
    public EnemyAttackState attackState;
    public EnemyReloadState reloadState;

    
    [HideInInspector]
    public Transform _player;
    [HideInInspector]
    public Transform _thisTransform;
    [SerializeField]
    private Transform _shootPoint;


    // Start is called before the first frame update
    void Start()
    {
        _currentState = idleState;
        _currentState.Enter(this);

        _player = GameObject.FindGameObjectWithTag("Player").transform;  
        _thisTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.UpdateState(this, Time.deltaTime);
    }


    public void TransitionToState(EnemyState state)
    {
        _currentState.Exit(this);
        _currentState = state;
        _currentState.Enter(this);
    }

    public void Shoot()
    {
        float currentSpread = Random.Range(0, _bulletSpread);

        Vector2 spread = Random.insideUnitCircle * currentSpread;
        Vector3 direction = _shootPoint.forward + spread.x * _shootPoint.right + spread.y * Vector3.up;
        direction.Normalize();

        Debug.DrawRay(_shootPoint.position, direction * 100, Color.red, .3f);
        if(Physics.Raycast(_shootPoint.position, direction, out RaycastHit hit, 100, _hitMask))
        {
            Debug.Log("Hit something");
            if(hit.collider.TryGetComponent(out TargetBehaviour targetBehaviour))
            {
                targetBehaviour.TakeDamage(1);
            }
        }

        Debug.Log("Shoot");
    }

    public bool PlayerWithinRange()
    {
        return Vector3.SqrMagnitude(_player.position - transform.position) < _attackDistance * _attackDistance;
    }

    public void RotateTowardsTarget(Vector3 target)
    {
        target.y = transform.position.y;

        transform.LookAt(target);
    }
}
