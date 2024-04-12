using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BirdBlockControlBehaviour : BirdControlState
{
    public BirdBlockControlBehaviour(BehaviourList behaviour) : base(behaviour)
    {
    }

    public override void EnterState(BirdControlBehaviour controlBehaviour)
    {
        controlBehaviour.SetSteeringBehaviour(_behaviour);
    }

    public override void UpdateState(BirdControlBehaviour controlBehaviour, float deltaTime)
    {
        controlBehaviour.SetTargetPosition(controlBehaviour.thisTransform.position);

        if(Vector3.SqrMagnitude(controlBehaviour.thisTransform.position - controlBehaviour.GetAverageFlockPos()) < 25)
        {
            Debug.Log("Blocking");
            controlBehaviour.health.SetInvincibility(1f);
        }
    }

    public override void ExitState(BirdControlBehaviour controlBehaviour)
    {
        //throw new System.NotImplementedException();
    }


    protected override void HandleBlockTransition(BirdControlBehaviour controlBehaviour, bool value)
    {
        if (!value)
            controlBehaviour.TransitionState(controlBehaviour._idle);
    }
}
