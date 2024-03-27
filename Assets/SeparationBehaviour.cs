using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SeparationBehaviour : SteeringBehaviour
{
    [SerializeField]
    private float separationStrength = 100f;

    public override Vector3 CalculateMovement(FlockAgent agentToMove, List<FlockAgent> context)
    {
        int contextCount = context.Count;

        if(contextCount == 0)
            return Vector3.zero;

        Vector3 separationVector = Vector3.zero;

        for(int i = 0; i < contextCount; i++)
        {
            Vector3 currentVector = (context[i].position - agentToMove.position);
            //Debug.DrawLine(agentToMove.position, agentToMove.position + currentVector);
            //Debug.Log(currentVector.sqrMagnitude);

            if(currentVector.sqrMagnitude != 0)
                separationVector -=  currentVector * (separationStrength/currentVector.sqrMagnitude);
        }

        separationVector/= contextCount;
        return separationVector;
    }
}
