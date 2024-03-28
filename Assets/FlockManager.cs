using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{

    private static List<FlockAgent> agents = new List<FlockAgent>();


    public static void AddAgent(FlockAgent agent)
    {
        agents.Add(agent);
    }


    private void Update()
    {
        FlockAgentOcttree.instance.CreateNewTree();
        AddAgentsToOcttree();

        List<FlockAgent> context = new List<FlockAgent>(8);
        foreach (FlockAgent agent in agents)
        {
            GetContext(agent, ref context);
            agent.CalculateMovement(context);
            context.Clear();
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
