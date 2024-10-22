using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BirdBlockControlBehaviour : BirdControlState
{
    [SerializeField]
    private float _cameraShakeMagnitude = .3f;
    [SerializeField, Range(0, 1)]
    private float _cameraShakeDamping = .3f;

    public BirdBlockControlBehaviour(BehaviourList behaviour) : base(behaviour)
    {
    }

    public override void EnterState(BirdControlBehaviour controlBehaviour)
    {
        controlBehaviour.SetSteeringBehaviour(_behaviour);
        controlBehaviour._anim.SetBool("Blocking", true);
        CameraShaker.instance.TriggerShake(99f, _cameraShakeMagnitude, _cameraShakeDamping);
    }

    public override void UpdateState(BirdControlBehaviour controlBehaviour, float deltaTime)
    {
        controlBehaviour.SetTargetPosition(controlBehaviour.thisTransform.position);

        if(Vector3.SqrMagnitude(controlBehaviour.thisTransform.position - controlBehaviour.GetAverageFlockPos()) < 25)
        {
            controlBehaviour.health.SetInvincibility(1f);
        }
    }

    public override void ExitState(BirdControlBehaviour controlBehaviour)
    {
        controlBehaviour._anim.SetBool("Blocking", false);
        CameraShaker.instance.CancleShake(_cameraShakeMagnitude);
    }


    protected override void HandleBlockTransition(BirdControlBehaviour controlBehaviour, bool value)
    {
        if (!value)
            controlBehaviour.TransitionState(controlBehaviour._idle);
    }
}
