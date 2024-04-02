using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{

    private static List<FlockAgent> agents = new List<FlockAgent>();

    [SerializeField]
    private BehaviourList behaviourList = null;


    [SerializeField]
    private List<SteeringBehaviourItems> steeringBehaviours = new List<SteeringBehaviourItems>();


    public static void AddAgent(FlockAgent agent)
    {
        agents.Add(agent);
    }


    private void Update()
    {
        HandleMovement();
    }


    private void HandleMovement()
    {
        FlockAgentOcttree.instance.CreateNewTree();
        AddAgentsToOcttree();

        float weightMultiplier = GetWeightMultiplier();

        MoveAgents(weightMultiplier);
    }


    private float GetWeightMultiplier()
    {
        int behaviourCount;
        float totalWeight = 0;


        if (!behaviourList)
        {
            behaviourCount = steeringBehaviours.Count;
            for (int i = 0; i < behaviourCount; i++)
            {
                totalWeight += steeringBehaviours[i].weight;
            }
        }
        else
        {
            behaviourCount = behaviourList.items.Count;
            for (int i = 0; i < behaviourCount; i++)
            {
                totalWeight += behaviourList.items[i].weight;
            }
        }
        

        return 1 / totalWeight;
    }


    private void MoveAgents(float weightMultiplier)
    {
        List<FlockAgent> context = new List<FlockAgent>(8);

        if(behaviourList)
        {
            int behaviourCount = behaviourList.items.Count;
            foreach (FlockAgent agent in agents)
            {
                GetContext(agent, ref context);
                agent.CalculateMovement(context, behaviourList.items, behaviourCount, weightMultiplier);
                context.Clear();
            }
        }
        else
        {
            int behaviourCount = steeringBehaviours.Count;
            foreach (FlockAgent agent in agents)
            {
                GetContext(agent, ref context);
                agent.CalculateMovement(context, steeringBehaviours, behaviourCount, weightMultiplier);
                context.Clear();
            }
        }
        
    }

    private void AddAgentsToOcttree()
    {
        if (!FlockAgentOcttree.instance)
            return;

        for(int i = 0; i < agents.Count; i++)
        {
            FlockAgentOcttree.instance.AddAgent(agents[i]);
        }
    }


    private void GetContext(FlockAgent agent, ref List<FlockAgent> context)
    {
        if (!FlockAgentOcttree.instance)
            return;

        FlockAgentOcttree.instance.GetAgentsInNode(agent, ref context);

        //Collider[] agentsInArea = Physics.OverlapSphere(agent.position, agent.sightRadius);
        //
        //foreach(Collider other in agentsInArea)
        //{
        //    if (other.TryGetComponent(out FlockAgent otherAgent))
        //    {
        //        if(Vector3.Dot(agent.thisTransform.forward, (otherAgent.position - agent.position).normalized) < agent.viewAngleCos)
        //            return;
        //
        //        context.Add(otherAgent);
        //    }
        //}
        //Debug.Log(agent.name + " has " + context.Count + " agents in range");
    }
}
