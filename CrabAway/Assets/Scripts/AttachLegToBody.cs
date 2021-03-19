using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachLegToBody : MonoBehaviour
{
    public Transform attachToBodyPoint;

    void Update()
    {
        gameObject.transform.position = attachToBodyPoint.position;
    }
}
