using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering.UI;

[System.Serializable]
public class EnemyAttackState : EnemyState
{
    [SerializeField]
    private int _maxBulletCount = 30;
    private int _currentBulletCount = 30;

    [SerializeField]
    private float _timeBetweenShot = .1f;
    private float _timer = 0f;

    [SerializeField]
    private float _bulletSpread = .3f;
    [SerializeField]
    private LayerMask _hitMask;
    [SerializeField]
    private Transform _shootPoint;
    [SerializeField]
    private GameObject _effect;


    public override void Enter(EnemyBehaviour behaviour)
    {
        //throw new System.NotImplementedException();
        behaviour._anim.SetBool("Attacking", true);
    }

    public override void UpdateState(EnemyBehaviour behaviour, float deltaTime)
    {
        behaviour.RotateTowardsTarget(behaviour._player.position);
        _timer -= deltaTime;
        if(_timer <= 0f)
        {
            behaviour.Shoot(_bulletSpread, _shootPoint, _hitMask, _effect);
            _currentBulletCount--;
            _timer = _timeBetweenShot;
        }

        if(_currentBulletCount <= 0)
        {
            behaviour.TransitionToState(behaviour.reloadState);
        }
    }

    public override void Exit(EnemyBehaviour behaviour)
    {
        behaviour._anim.SetBool("Attacking", false);
        //throw new System.NotImplementedException();
    }


    public void ReloadBullets()
    {
        _currentBulletCount = _maxBulletCount;
    }
    
}
