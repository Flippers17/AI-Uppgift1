using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockAgent : MonoBehaviour
{
    [HideInInspector]
    public Transform thisTransform;
    [HideInInspector]
    public Vector3 position;

    [HideInInspector]
    public Vector3 velocity;
    [SerializeField]
    private float _maxSpeed = 5f;

    public float sightRadius = 2f;
    [SerializeField, Range(0f, 180f)]
    private float viewAngle = 180;
    [HideInInspector, SerializeField]
    public float viewAngleCos = 0f;

    public List<SteeringBehaviourItems> steeringBehaviours = new List<SteeringBehaviourItems>();

    private float _totalWeight = 1f;



    private void OnEnable()
    {
        thisTransform = transform;
        velocity = thisTransform.forward * _maxSpeed;
        position = thisTransform.position;
        FlockManager.AddAgent(this);
    }

    private void OnValidate()
    {
        if(steeringBehaviours.Count > 0)
        {
            _totalWeight = 0;
            for(int i = 0; i < steeringBehaviours.Count; i++)
            {
                _totalWeight += steeringBehaviours[i].weight;
            }
        }

        viewAngleCos = Mathf.Cos(viewAngle);
    }


    private void Update()
    {
        thisTransform.position += velocity * Time.deltaTime;
        position = thisTransform.position;
        thisTransform.forward = velocity;
    }

    public void CalculateMovement(List<FlockAgent> context)
    {
        float deltaTime = Time.deltaTime;
        Vector3 force = Vector3.zero;

        for(int i = 0; i < steeringBehaviours.Count; i++)
        {
            force += steeringBehaviours[i].behaviour.CalculateMovement(this, context) * steeringBehaviours[i].weight/_totalWeight;
        }

        force = force * Time.deltaTime;
        Vector3 newVelocity = velocity + force;

        if(newVelocity.sqrMagnitude > _maxSpeed * _maxSpeed)
            newVelocity = newVelocity.normalized * _maxSpeed;

        velocity = newVelocity;
    }
}


[System.Serializable]
public class SteeringBehaviourItems
{
    public SteeringBehaviour behaviour;
    public float weight = 1f;
}
