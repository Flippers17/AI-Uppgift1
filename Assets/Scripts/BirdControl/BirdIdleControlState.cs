using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BirdIdleControlState : BirdControlState
{
    public BirdIdleControlState(BehaviourList behaviour) : base(behaviour)
    {
    }

    public override void EnterState(BirdControlBehaviour controlBehaviour)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(BirdControlBehaviour controlBehaviour)
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState(BirdControlBehaviour controlBehaviour)
    {
        throw new System.NotImplementedException();
    }
}
