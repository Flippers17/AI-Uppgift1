using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BirdControlState
{
    [SerializeField]
    protected BehaviourList _behaviour;
    
    protected BirdControlState(BehaviourList behaviour)
    {
        _behaviour = behaviour;
    }
    
    public abstract void EnterState(BirdControlBehaviour controlBehaviour);
    
    public abstract void UpdateState(BirdControlBehaviour controlBehaviour);
    
    public abstract void ExitState(BirdControlBehaviour controlBehaviour);
}
