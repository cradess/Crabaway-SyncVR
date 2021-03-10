using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


public class PlayerAgent : MonoBehaviour
{
    NavMeshAgent playerAgent;
    UnityEvent m_OnReachDestination;

    private Transform targetPos;
    [SerializeField] private Transform[] targets;

    private int i = 0;

    void Start()
    {
        playerAgent = gameObject.GetComponent<NavMeshAgent>();

        if (m_OnReachDestination == null)
            m_OnReachDestination = new UnityEvent();

        m_OnReachDestination.AddListener(DestinationReached);

        changeAgentGoal(targets[i]);
    }

    public void Update()
    {
        if (Vector3.Distance(targetPos.position, transform.position) <= 0.2f && i < targets.Length - 1)
        {
            m_OnReachDestination.Invoke();
        }
        else if(i == targets.Length - 1)
        {
            playerAgent.autoBraking = true;
        }
    }

    public void changeAgentGoal(Transform target)
    {
        targetPos = target;
        playerAgent.destination = target.position;
    }

    private void DestinationReached()
    {
        i++;
        changeAgentGoal(targets[i]);
    }
}
