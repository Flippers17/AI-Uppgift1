using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BirdTargetControlBehaviour : BirdControlState
{
    [SerializeField] private Vector3 _interactionSize;
    
    public BirdTargetControlBehaviour(BehaviourList behaviour) : base(behaviour)
    {
    }

    public override void EnterState(BirdControlBehaviour controlBehaviour)
    {
        controlBehaviour.SetSteeringBehaviour(_behaviour);
        controlBehaviour.SetInteraction(FlockInteractionType.Interact, _interactionSize);
    }

    public override void UpdateState(BirdControlBehaviour controlBehaviour, float deltaTime)
    {
        controlBehaviour.MoveTargetAtAim();
    }

    public override void ExitState(BirdControlBehaviour controlBehaviour)
    {
        //throw new System.NotImplementedException();
        controlBehaviour.SetInteraction(FlockInteractionType.None, Vector3.zero);
        controlBehaviour.StopCurrentInteraction();
    }

    protected override void HandleFineControlTransition(BirdControlBehaviour behaviour, bool value)
    {
        if (!value)
            behaviour.TransitionState(behaviour._idle);
    }

    protected override void HandleAttackTransition(BirdControlBehaviour controlBehaviour)
    {
        Vector3 averagePos = controlBehaviour.GetAverageFlockPos();

        if (Vector3.SqrMagnitude(averagePos - controlBehaviour.thisTransform.position) > 64f)
            return;

        controlBehaviour.TransitionState(controlBehaviour._attackState);
    }
}
