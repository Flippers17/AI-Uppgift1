using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SelectionBase]
public class EnemyBehaviour : MonoBehaviour
{

    public Animator _anim;


    private EnemyState _currentState;

    [SerializeField]
    private float _attackDistance = 50f;
    


    public EnemyIdleState idleState;
    public EnemyAttackState attackState;
    public EnemyReloadState reloadState;

    
    [HideInInspector]
    public Transform _player;
    [HideInInspector]
    public Transform _thisTransform;


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

    public void Shoot(float bulletSpread, Transform shootPoint, LayerMask hitMask, GameObject effect)
    {
        float currentSpread = Random.Range(0, bulletSpread);

        Vector2 spread = Random.insideUnitCircle * currentSpread;
        Vector3 direction = shootPoint.forward + spread.x * shootPoint.right + spread.y * Vector3.up;
        direction.Normalize();

        Debug.DrawRay(shootPoint.position, direction * 100, Color.red, .3f);
        Transform effectTransform = Instantiate(effect, shootPoint.position, shootPoint.rotation).transform;
        effectTransform.forward = direction;

        if(Physics.Raycast(shootPoint.position, direction, out RaycastHit hit, 100, hitMask))
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
