using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsBounds : MonoBehaviour
{
    [SerializeField]
    private BoxCollider boundCollider;

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out FlockAgent agent))
        {
            float newX = agent.position.x;
            float newY = agent.position.y;
            float newZ = agent.position.z;

            if(agent.position.x >= boundCollider.size.x/2)
            {
                newX = -boundCollider.size.x/2;
            }
            else if(agent.position.x <= -boundCollider.size.x/2)
            {
                newX = boundCollider.size.x / 2;
            }

            if (agent.position.y >= boundCollider.size.y / 2)
            {
                newY = -boundCollider.size.y / 2;
            }
            else if (agent.position.y <= -boundCollider.size.y / 2)
            {
                newY = boundCollider.size.y / 2;
            }


            if (agent.position.z >= boundCollider.size.z / 2)
            {
                newZ = -boundCollider.size.z / 2;
            }
            else if (agent.position.z <= -boundCollider.size.z / 2)
            {
                newZ = boundCollider.size.z / 2;
            }


            agent.thisTransform.position = new Vector3(newX, newY, newZ);
        }
    }
}
