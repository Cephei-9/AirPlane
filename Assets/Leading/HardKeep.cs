using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HardKeep : MonoBehaviour
{   
    public Transform bodyTransform;
    public Rigidbody bodyRigidbody;

    public Rigidbody airPlaneRb;

    private void FixedUpdate()
    {    
        KeepAndMove();
    }

    private void KeepAndMove()
    {
        Vector3 toBody = bodyTransform.position - transform.position;
        Vector3 toBodyOnPlane = Vector3.ProjectOnPlane(toBody, transform.up);

        Debug.DrawRay(transform.position, toBodyOnPlane, Color.red);

        bodyRigidbody.velocity = Vector3.Project(bodyRigidbody.velocity, transform.up);
        bodyRigidbody.AddForce((-toBodyOnPlane) * (1 / Time.fixedDeltaTime), ForceMode.VelocityChange);

        Vector3 stabilizatorVelosityOnPlane = Vector3.ProjectOnPlane(airPlaneRb.velocity, transform.up);
        bodyRigidbody.velocity += stabilizatorVelosityOnPlane;

        bodyTransform.rotation = transform.rotation;
    }
}
