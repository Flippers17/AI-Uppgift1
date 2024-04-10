using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BirdIdleControlState : BirdControlState
{
    [SerializeField]
    private Vector3 _position;

    public BirdIdleControlState(BehaviourList behaviour) : base(behaviour)
    {
    }

    public override void EnterState(BirdControlBehaviour controlBehaviour)
    {
        controlBehaviour.SetSteeringBehaviour(_behaviour);
    }

    public override void UpdateState(BirdControlBehaviour controlBehaviour, float deltaTime)
    {
        controlBehaviour.SetTargetPosition(controlBehaviour.thisTransform.position + _position);
    }

    public override void ExitState(BirdControlBehaviour controlBehaviour)
    {
        //throw new System.NotImplementedException();
    }
}
