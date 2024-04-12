using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ObstacleAvoidanceBehaviour : SteeringBehaviour
{
    [SerializeField]
    private int numberOfRays = 1000;
    
    [SerializeField]
    private float turnFactor = 1f;

    [SerializeField]
    private float avoidanceForce = 10f;

    [NonSerialized]
    private List<Vector3> avoidanceRaysDirections = new List<Vector3>();

    [NonSerialized]
    private bool hasGeneratedRays = false;


    public override Vector3 CalculateMovement(FlockAgent agentToMove, List<FlockAgent> context, float forceMultiplier)
    {
        if(!hasGeneratedRays)
            GenerateRays();

        Vector3 bestDir = agentToMove.forward;
        float furthestUnobstructedDist = 0f;
        RaycastHit hit;


        Vector3 agentPos = agentToMove.position;
        Vector3 currentDir = Vector3.zero;


        for(int i = 0; i < numberOfRays; i++) 
        {
            currentDir = avoidanceRaysDirections[i];
            currentDir = agentToMove.thisTransform.TransformDirection(currentDir);
            //Debug.Log(currentDir);

            if (Physics.Raycast(agentPos, currentDir, out hit, agentToMove.sightRadius, agentToMove.avoidanceMask))
            {
                //Debug.DrawRay(agentPos, currentDir * agentToMove.sightRadius, Color.red);
                if (hit.distance > furthestUnobstructedDist)
                {
                    bestDir = currentDir;
                    furthestUnobstructedDist = hit.distance;
                    
                }
            }
            else
            {
                if(i == 0)
                    return Vector3.zero;

                //Debug.DrawRay(agentToMove.position, currentDir * agentToMove.sightRadius, Color.green);
                return currentDir * (avoidanceForce * forceMultiplier);
            }
        }

        return bestDir * (avoidanceForce * forceMultiplier);

    }

    private void GenerateRays()
    {
        for(int i = 0; i < numberOfRays; i++)
        {
            var k = i + .5f;

            var phi = Mathf.Acos(1f - 2f * k / numberOfRays);
            var theta = Mathf.PI * (1 + Mathf.Sqrt(5)) * k;

            float x = Mathf.Cos(theta) * Mathf.Sin(phi);
            float y = Mathf.Sin(theta) * Mathf.Sin(phi);
            float z = Mathf.Cos(phi);

            avoidanceRaysDirections.Add(new Vector3(x, y, z));
            //Debug.Log(avoidanceRaysDirections.Count);
            //Debug.DrawRay(Vector3.zero, new Vector3(x, y, z), Color.green * (1 - (float)i/numberOfRays), 50f);
            //Debug.Log(new Vector3(x, y, z));

            /*
            //float t = i/ (numberOfRays - 1f);
            float t = i/ (numberOfRays);
            float inclination = Mathf.Acos(1 - 2 * t);
            float azimuth = 2 * Mathf.PI * turnFactor * i;
            
            float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
            float y = Mathf.Cos(inclination) * Mathf.Cos(azimuth);
            float z = Mathf.Sin(inclination);

            avoidanceRaysDirections.Add(new Vector3(x, y, z));*/
        }

        hasGeneratedRays = true;
    }
}
