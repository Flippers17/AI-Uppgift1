using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BirdAttackState : BirdControlState
{
    private float _attackTimer;

    [SerializeField] private Vector3 _interactionSize;
    [SerializeField] private float _cameraShakeMagnitude = .1f;
    [SerializeField, Range(0, 1)] private float _cameraShakeDamping = .3f;
    [SerializeField] private AudioPlayer _audioPlayer;



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
        
        if(_audioPlayer)
            _audioPlayer.PlaySound();

        CameraShaker.instance.TriggerShake(99f, _cameraShakeMagnitude, _cameraShakeDamping);
    }

    public override void UpdateState(BirdControlBehaviour controlBehaviour, float deltaTime)
    {
        _attackTimer -= deltaTime;

        if(_attackTimer < 0)
        {
            controlBehaviour.TransitionState(controlBehaviour._idle);
        }
    }

    public override void ExitState(BirdControlBehaviour controlBehaviour)
    {
        controlBehaviour.SetInteraction(FlockInteractionType.None, Vector3.zero);
        CameraShaker.instance.CancleShake(_cameraShakeMagnitude);
    }


    //The transitions below are not implemented for this state. This is intentional
    protected override void HandleAttackTransition(BirdControlBehaviour controlBehaviour)
    {
        
    }

    protected override void HandleFineControlTransition(BirdControlBehaviour controlBehaviour, bool value)
    {
        
    }
}
