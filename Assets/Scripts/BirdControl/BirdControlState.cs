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
    
    public abstract void UpdateState(BirdControlBehaviour controlBehaviour, float deltaTime);
    
    public abstract void ExitState(BirdControlBehaviour controlBehaviour);

    public virtual void OnControlEvent(BirdControlBehaviour behaviour, ControlEvent controlEvent, bool value)
    {
        switch (controlEvent)
        {
            case ControlEvent.Attack:
                HandleAttackTransition(behaviour);
                break;

            case ControlEvent.FineControl:
                HandleFineControlTransition(behaviour, value);
                break;

            case ControlEvent.Block:
                HandleBlockTransition(behaviour, value);
                break;
        }
    }

    protected virtual void HandleAttackTransition(BirdControlBehaviour controlBehaviour)
    {
        Vector3 averagePos = controlBehaviour.GetAverageFlockPos();

        if (Vector3.SqrMagnitude(averagePos - controlBehaviour.GetTargetPosition()) > 64f)
            return;

        controlBehaviour.TransitionState(controlBehaviour._attackState);
    }

    protected virtual void HandleFineControlTransition(BirdControlBehaviour controlBehaviour, bool value)
    {
        if (value)
        {
            controlBehaviour.TransitionState(controlBehaviour._targetingState);
        }
    }

    protected virtual void HandleBlockTransition(BirdControlBehaviour controlBehaviour, bool value)
    {
        if (value)
        {
            controlBehaviour.TransitionState(controlBehaviour._blockState);
        }
    }
}

public enum ControlEvent
{
    Attack,
    Block,
    FineControl
}
