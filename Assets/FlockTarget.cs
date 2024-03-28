using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockTarget : MonoBehaviour
{
    [SerializeField]
    private TargetSteeringBehaviour _targetSteeringBehaviour;

    private void OnEnable()
    {
        if (_targetSteeringBehaviour)
            _targetSteeringBehaviour.Target = transform;
    }
}
