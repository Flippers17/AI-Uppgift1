using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SeparationBehaviour : SteeringBehaviour
{
    [SerializeField]
    private float separationStrength = 100f;

    public override Vector3 CalculateMovement(FlockAgent agentToMove, List<FlockAgent> context, float forceMultiplier)
    {
        int contextCount = context.Count;

        if(contextCount == 0)
            return Vector3.zero;

        Vector3 separationVector = Vector3.zero;

        Vector3 currentVector;

        for(int i = 0; i < contextCount; i++)
        {
            currentVector = (context[i].position - agentToMove.position);

            float currentSquareMagnitude = currentVector.sqrMagnitude;
            if (currentSquareMagnitude != 0)
                separationVector -= currentVector * ((separationStrength * forceMultiplier) / currentSquareMagnitude);
        }

        separationVector/= contextCount;
        return separationVector;
    }
}
