using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyReloadState : EnemyState
{
    [SerializeField]
    private float _reloadTime = 3f;
    private float _timer = 0;


    public override void Enter(EnemyBehaviour behaviour)
    {
        _timer = _reloadTime;
        behaviour._anim.SetBool("Reloading", true);
    }

    public override void UpdateState(EnemyBehaviour behaviour, float deltaTime)
    {
        _timer -= deltaTime;
        if (_timer <= 0)
        {
            behaviour.TransitionToState(behaviour.idleState);
        }
    }

    public override void Exit(EnemyBehaviour behaviour)
    {
        behaviour.attackState.ReloadBullets();
        behaviour._anim.SetBool("Reloading", false);
    }

    
}
