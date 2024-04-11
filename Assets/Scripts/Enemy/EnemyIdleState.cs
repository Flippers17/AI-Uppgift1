using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[System.Serializable]
public class EnemyIdleState : EnemyState
{
    public override void Enter(EnemyBehaviour behaviour)
    {
        behaviour._anim.SetBool("Idle", true);
    }

    public override void UpdateState(EnemyBehaviour behaviour, float deltaTime)
    {
        if (behaviour.PlayerWithinRange())
        {
            behaviour.TransitionToState(behaviour.attackState);
        }
    }

    public override void Exit(EnemyBehaviour behaviour)
    {
        behaviour._anim.SetBool("Idle", false);
    }

    
}
