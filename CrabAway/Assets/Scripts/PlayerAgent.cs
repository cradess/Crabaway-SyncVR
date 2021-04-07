﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;


public class PlayerAgent : MonoBehaviour
{
    NavMeshAgent playerAgent;
    UnityEvent m_OnReachDestination;

    private Transform targetPos;
    [SerializeField] public Transform[] targets;

    public GameObject crab;

    private int i = 0;

    public Text testingDoneText;

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
        //testingDoneText.enabled = true;
        if (Vector3.Distance(targetPos.position, transform.position) <= 0.8f && i < targets.Length)
        {
            m_OnReachDestination.Invoke();
        }
        if(i == targets.Length - 1)
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

        if (i == 3)
        {
            testingDoneText.gameObject.SetActive(true);
        }

        changeAgentGoal(targets[i]);
    }
}
