using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class FlockAgentOcttree : MonoBehaviour
{
    public static FlockAgentOcttree instance;

    [SerializeField, HideInInspector]
    private Bounds rootBounds = new Bounds();

    [SerializeField]
    private Vector3 _rootSize = new Vector3(5, 5, 5);

    private FlockAgentOcttreeNode _rootNode;

    [SerializeField]
    private int capacity = 4;
    
    [SerializeField]
    private int maxLevels = 6;

    private void OnEnable()
    {
        instance = this;

        rootBounds.center = transform.position;
        rootBounds.size = _rootSize;
        _rootNode = new FlockAgentOcttreeNode(rootBounds, capacity, maxLevels);
    }

    public void CreateNewTree()
    {
        _rootNode = new FlockAgentOcttreeNode(rootBounds, capacity, maxLevels);
    }


    public void AddAgent(FlockAgent agent)
    {
        _rootNode.AddAgent(agent, agent.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (_rootNode == null)
        {
            Gizmos.DrawWireCube(transform.position, _rootSize);
            return;
        }

        List<Bounds> allBounds = new List<Bounds>();
        _rootNode.GetWholeTree(ref allBounds);
        
        for(int i = 0; i < allBounds.Count; ++i)
        {
            Gizmos.DrawWireCube(allBounds[i].center, allBounds[i].size);
        }
    }
}


public class FlockAgentOcttreeNode 
{ 
    public bool isDivided = false;

    public Bounds bounds;

    private Vector3 halfSize;
    private Vector3 quarterSize;

    public int capacity = 4;

    public FlockAgentOcttreeNode topNorthEast = null;
    public FlockAgentOcttreeNode topNorthWest = null;
    public FlockAgentOcttreeNode topSouthEast = null;
    public FlockAgentOcttreeNode topSouthWest = null;
    public FlockAgentOcttreeNode bottomNorthEast = null;
    public FlockAgentOcttreeNode bottomNorthWest = null;
    public FlockAgentOcttreeNode bottomSouthEast = null;
    public FlockAgentOcttreeNode bottomSouthWest = null;

    public List<FlockAgent> agents = new List<FlockAgent>();
    private int currentCount = 0;

    private int levelsLeft = 0;

    public FlockAgentOcttreeNode(Bounds Bounds, int capacity, int levelsLeft)
    {
        this.bounds = Bounds;
        halfSize = new Vector3(bounds.size.x / 2, bounds.size.y / 2, bounds.size.z / 2);
        quarterSize = new Vector3(halfSize.x / 2, halfSize.z / 2, halfSize.z / 2);
        this.capacity = capacity;
        this.levelsLeft = levelsLeft;
    }

    public virtual void AddAgent(FlockAgent agent, Vector3 position) 
    {
        if(!bounds.Contains(position)) 
            return;

        if(isDivided)
        {
            topNorthEast.AddAgent(agent, position);
            topNorthWest.AddAgent(agent, position);
            topSouthEast.AddAgent(agent, position);
            topSouthWest.AddAgent(agent, position);
            bottomNorthEast.AddAgent(agent, position);
            bottomNorthWest.AddAgent(agent, position);
            bottomSouthEast.AddAgent(agent, position);
            bottomSouthWest.AddAgent(agent, position);

            return;
        }

        agents.Add(agent);
        currentCount++;

        if(currentCount > capacity && levelsLeft > 0)
        {
            Divide();
        }
    }


    public void GetAgentsAtPosition(Vector3 position, ref List<FlockAgent> agentsInNodes)
    {
        if (!bounds.Contains(position))
            return;

        if (isDivided)
        {
            topNorthEast.GetAgentsAtPosition(position, ref agentsInNodes);
            topNorthWest.GetAgentsAtPosition(position, ref agentsInNodes);
            topSouthEast.GetAgentsAtPosition(position, ref agentsInNodes);
            topSouthWest.GetAgentsAtPosition(position, ref agentsInNodes);
            bottomNorthEast.GetAgentsAtPosition(position, ref agentsInNodes);
            bottomNorthWest.GetAgentsAtPosition(position, ref agentsInNodes);
            bottomSouthEast.GetAgentsAtPosition(position, ref agentsInNodes);
            bottomSouthWest.GetAgentsAtPosition(position, ref agentsInNodes);
            return;
        }

        for(int i = 0; i < currentCount; i++)
        {
            agentsInNodes.Add(agents[i]);
        }
    }


    public void Divide()
    {
        if (isDivided)
            return;

        Bounds topNorthEastBound = new Bounds(bounds.center + new Vector3(quarterSize.x, quarterSize.y, quarterSize.z), halfSize);
        topNorthEast = new FlockAgentOcttreeNode(topNorthEastBound, capacity, levelsLeft - 1);
        
        Bounds topNorthWestBound = new Bounds(bounds.center + new Vector3(-quarterSize.x, quarterSize.y, quarterSize.z), halfSize);
        topNorthWest = new FlockAgentOcttreeNode(topNorthWestBound, capacity, levelsLeft - 1);

        Bounds topSouthEastBound = new Bounds(bounds.center + new Vector3(quarterSize.x, quarterSize.y, -quarterSize.z), halfSize);
        topSouthEast = new FlockAgentOcttreeNode(topSouthEastBound, capacity, levelsLeft - 1);

        Bounds topSouthWestBound = new Bounds(bounds.center + new Vector3(-quarterSize.x, quarterSize.y, -quarterSize.z), halfSize);
        topSouthWest = new FlockAgentOcttreeNode(topSouthWestBound, capacity, levelsLeft - 1);

        Bounds bottomNorthEastBound = new Bounds(bounds.center + new Vector3(quarterSize.x, -quarterSize.y, quarterSize.z), halfSize);
        bottomNorthEast = new FlockAgentOcttreeNode(bottomNorthEastBound, capacity, levelsLeft - 1);

        Bounds bottomNorthWestBound = new Bounds(bounds.center + new Vector3(-quarterSize.x, -quarterSize.y, quarterSize.z), halfSize);
        bottomNorthWest = new FlockAgentOcttreeNode(bottomNorthWestBound, capacity, levelsLeft - 1);

        Bounds bottomSouthEastBound = new Bounds(bounds.center + new Vector3(quarterSize.x, -quarterSize.y, -quarterSize.z), halfSize);
        bottomSouthEast = new FlockAgentOcttreeNode(bottomSouthEastBound, capacity, levelsLeft - 1);

        Bounds bottomSouthWestBound = new Bounds(bounds.center + new Vector3(-quarterSize.x, -quarterSize.y, -quarterSize.z), halfSize);
        bottomSouthWest = new FlockAgentOcttreeNode(bottomSouthWestBound, capacity, levelsLeft - 1);

        isDivided = true;

        for(int i = agents.Count - 1; i >= 0; i--)
        {
            FlockAgent agent = agents[i];
            agents.RemoveAt(i);
            currentCount--;
            AddAgent(agent, agent.position);
        }
    }


    //public bool ContainsSphere(Vector3 center, float radius)
    //{
    //    if (radius == 0)
    //        return bounds.Contains(center);
    //
    //    if()
    //}

    public void GetWholeTree(ref List<Bounds> allBounds)
    {
        if (isDivided)
        {
            topNorthEast.GetWholeTree(ref allBounds);
            topNorthWest.GetWholeTree(ref allBounds);
            topSouthEast.GetWholeTree(ref allBounds);
            topSouthWest.GetWholeTree(ref allBounds);
            bottomNorthEast.GetWholeTree(ref allBounds);
            bottomNorthWest.GetWholeTree(ref allBounds);
            bottomSouthEast.GetWholeTree(ref allBounds);
            bottomSouthWest.GetWholeTree(ref allBounds);
            return;
        }

        allBounds.Add(bounds);
    }
}
