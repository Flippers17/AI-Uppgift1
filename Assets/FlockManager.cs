using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{

    public static FlockManager mainFlock;

    [SerializeField]
    private bool _isMainFlock = false;

    [SerializeField]
    private FlockAgentOcttree _octtree;

    [SerializeField]
    private List<FlockAgent> _agents = new List<FlockAgent>();

    [Space(15), SerializeField]
    private BehaviourList _behaviourList = null;


    [SerializeField]
    private List<SteeringBehaviourItems> _steeringBehaviours = new List<SteeringBehaviourItems>();

    [SerializeField, Tooltip("If a Target steering behaviour is used, it's target would be this")]
    private Transform _target;
    [SerializeField]
    private TargetSteeringBehaviour _targetSteeringBehaviour;


    private void OnEnable()
    {
        if(_isMainFlock)
            mainFlock = this;
    }


    public void AddAgent(FlockAgent agent)
    {
        _agents.Add(agent);
    }


    private void Update()
    {
        HandleMovement();
    }


    private void HandleMovement()
    {
        if(_targetSteeringBehaviour)
            _targetSteeringBehaviour.Target = _target;  

        _octtree.CreateNewTree();
        AddAgentsToOcttree();

        float weightMultiplier = GetWeightMultiplier();

        MoveAgents(weightMultiplier);
    }


    private float GetWeightMultiplier()
    {
        int behaviourCount;
        float totalWeight = 0;


        if (!_behaviourList)
        {
            behaviourCount = _steeringBehaviours.Count;
            for (int i = 0; i < behaviourCount; i++)
            {
                totalWeight += _steeringBehaviours[i].weight;
            }
        }
        else
        {
            behaviourCount = _behaviourList.items.Count;
            for (int i = 0; i < behaviourCount; i++)
            {
                totalWeight += _behaviourList.items[i].weight;
            }
        }
        

        return 1 / totalWeight;
    }


    private void MoveAgents(float weightMultiplier)
    {
        List<FlockAgent> context = new List<FlockAgent>(8);

        if(_behaviourList)
        {
            int behaviourCount = _behaviourList.items.Count;
            foreach (FlockAgent agent in _agents)
            {
                GetContext(agent, ref context);
                agent.CalculateMovement(context, _behaviourList.items, behaviourCount, weightMultiplier);
                context.Clear();
            }
        }
        else
        {
            int behaviourCount = _steeringBehaviours.Count;
            foreach (FlockAgent agent in _agents)
            {
                GetContext(agent, ref context);
                agent.CalculateMovement(context, _steeringBehaviours, behaviourCount, weightMultiplier);
                context.Clear();
            }
        }
        
    }

    private void AddAgentsToOcttree()
    {
        if (!_octtree)
            return;

        for(int i = 0; i < _agents.Count; i++)
        {
            _octtree.AddAgent(_agents[i]);
        }
    }


    private void GetContext(FlockAgent agent, ref List<FlockAgent> context)
    {
        if (!_octtree)
            return;

        _octtree.GetAgentsInNode(agent, ref context);

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

    public void SetTarget(Transform target)
    {
        _target = target;
    }


    public void SetSteeringBehaviour(BehaviourList behaviourList)
    {
        _behaviourList = behaviourList;
    }
}
