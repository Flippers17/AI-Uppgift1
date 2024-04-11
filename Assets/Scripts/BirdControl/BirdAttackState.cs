using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BirdAttackState : BirdControlState
{
    private float _attackTimer;

    [SerializeField] private Vector3 _interactionSize;

    public BirdAttackState(BehaviourList behaviour) : base(behaviour)
    {
    }

    public override void EnterState(BirdControlBehaviour controlBehaviour)
    {
        controlBehaviour.MoveTargetAtAim();
        float distance = Vector3.Distance(controlBehaviour.GetAimPointPos(), controlBehaviour.GetAverageFlockPos());
        _attackTimer = (distance / _behaviour.maxSpeed) + .2f;
        controlBehaviour.SetSteeringBehaviour(_behaviour);
        controlBehaviour.SetInteraction(FlockInteractionType.Attack, _interactionSize);
    }

    public override void UpdateState(BirdControlBehaviour controlBehaviour, float deltaTime)
    {
        _attackTimer -= deltaTime;

        if(_attackTimer < 0)
        {
            controlBehaviour.TransitionToIdle();
        }
    }

    public override void ExitState(BirdControlBehaviour controlBehaviour)
    {
        //throw new System.NotImplementedException();
    }
}
