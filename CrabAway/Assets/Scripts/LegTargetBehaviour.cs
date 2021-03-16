using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegTargetBehaviour : MonoBehaviour
{
    public Transform legPos;

    private RaycastHit hit;

    private Vector3 targetLocation;

    public float transitionSpeed;

    private Rigidbody legPosRigidbody;

    private bool canMove = false;

    public float stepDistance;

    private void Start()
    {
        legPosRigidbody = legPos.GetComponent<Rigidbody>();
    }

    void Update()
    {
        LegTargetRaycastDown();
        LegTargetDistance();
        MoveLegPos();
    }

    private void LegTargetRaycastDown()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, 1 << 8))
        {
            targetLocation = hit.point;
        }

        targetLocation += new Vector3(0, transform.localScale.y / 15, 0);

        transform.position = targetLocation;
    }

    private void LegTargetDistance()
    {
        if(Vector3.Distance(legPos.position, transform.position) > stepDistance)
        {
            canMove = true;
        }
    }

    private void MoveLegPos()
    {
        if (canMove)
        {
            LerpPosition(transform.position, 0.1f);
            canMove = false;
        }
    }

    private void LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = legPos.position;

        while (time < duration)
        {
            legPos.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
        }
        legPos.position = targetPosition;
    }
}
