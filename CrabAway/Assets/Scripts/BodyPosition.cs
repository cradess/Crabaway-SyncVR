using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BodyPosition : MonoBehaviour
{
    public Transform[] listOfLegPositions; //= new List<Transform>();

    private float combinedLegYPos;
    private float averageLegYPos;

    private float combinedLeftLegsYPos;
    private float combinedRightLegsYPos;
    private Vector3 averageLeftLegsYPos;
    private Vector3 averageRightLegsYPos;

    public float offset;

    public NavMeshAgent plAgent;

    private Vector3 bodyDirection;
    private Quaternion bodyRotation;

    void Start()
    {
        UpdateBodyYPosition();
    }
    private void Update()
    {
        Debug.Log("averageRightLegsYPos: " + averageRightLegsYPos +
                  "         averageLeftLegsYPos: " + averageLeftLegsYPos);

        UpdateBodyPosition();
        UpdateBodyRotation();
    }

    public void UpdateBodyPosition()
    {
        transform.position = new Vector3(plAgent.transform.position.x, transform.position.y, plAgent.transform.position.z);
    }
    public void UpdateBodyRotation()
    {
        bodyDirection = plAgent.destination - transform.position;
        bodyRotation = Quaternion.LookRotation(bodyDirection) * Quaternion.Euler(0, 90f, 0);
        bodyRotation.x = 0;

        /////

        combinedRightLegsYPos = 0;
        combinedLeftLegsYPos = 0;

        averageRightLegsYPos.x = listOfLegPositions[5].localPosition.x;
        averageLeftLegsYPos.x = listOfLegPositions[1].localPosition.x;

        averageRightLegsYPos.z = listOfLegPositions[5].localPosition.x;
        averageLeftLegsYPos.z = listOfLegPositions[1].localPosition.x;


        for (int i = 4; i < listOfLegPositions.Length; i++)
        {
            combinedRightLegsYPos += listOfLegPositions[i].transform.localPosition.y;
        }

        for (int i = 0; i < listOfLegPositions.Length - 4; i++)
        {
            combinedLeftLegsYPos += listOfLegPositions[i].transform.localPosition.y;
        }

        averageRightLegsYPos.y = combinedRightLegsYPos / 4;
        averageLeftLegsYPos.y = combinedLeftLegsYPos / 4;

        var test = Quaternion.FromToRotation(Vector3.up, averageLeftLegsYPos - averageRightLegsYPos).eulerAngles.y ;//Vector3.Angle(averageLeftLegsYPos, averageRightLegsYPos);
        Debug.Log("test angle: " + test);
        /////
        
        bodyRotation.z = 0;

        transform.rotation = Quaternion.Lerp(transform.rotation, bodyRotation, 0.2f * Time.deltaTime);
    }

    public void UpdateBodyYPosition()
    {
        combinedLegYPos = 0;

        foreach (Transform leg in listOfLegPositions)
        {
            combinedLegYPos += leg.position.y;
        }

        averageLegYPos = combinedLegYPos / 8;

        if (Mathf.Abs(transform.position.y - averageLegYPos) > 1.1f || Mathf.Abs(transform.position.y - averageLegYPos) < 0.9f)
        {
           transform.position = new Vector3(transform.position.x, averageLegYPos + offset, transform.position.z);
        }
    }
}
