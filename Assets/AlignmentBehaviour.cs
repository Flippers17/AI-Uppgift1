using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AlignmentBehaviour : SteeringBehaviour
{
    public override Vector3 CalculateMovement(FlockAgent agentToMove, List<FlockAgent> context)
    {
        int contextCount = context.Count;
        if(contextCount == 0)
            return Vector3.zero;

        Vector3 averageVelocity = Vector3.zero;

        for(int i = 0; i < contextCount; i++)
        {
            averageVelocity += context[i].velocity;
        }

        averageVelocity /= contextCount;

        return averageVelocity;
    }
}
