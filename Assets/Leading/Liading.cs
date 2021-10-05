using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liading : MonoBehaviour
{
    /*
     * Попробовать активировать приземление при колизии 
     */

    public Transform body;
    public Rigidbody bodyRb;
    [Space]
    public Transform airPlane;
    public Rigidbody airPlaneRb;
    [Space]
    public HardKeep hardKeep;
    public Stabilizator stabilizator;

    private bool isEndLeading = false;
    private bool isLeading = false;

    private void Start()
    {
        StartLeading();
        isEndLeading = false;

        FindObjectOfType<AirPlane>().OnEngineActiveChanged += OnEngineActiveChanged;

        FindObjectOfType<CollisionDetecter>().OnCollision += OnCollision;
    }

    private void Update()
    {
        //StopLeading();
    }

    private void OnEngineActiveChanged(bool active)
    {
        if (active) EndLeading();
        else StartLeading();
    }

    private void OnCollision(Collision collision)
    {
        StartLeading();
    }

    public void StartLeading()
    {
        if (isLeading == false)
        {
            body.parent = null;
            isEndLeading = false;
            SetActiveComponent(true);

            stabilizator.SetVelosity(airPlaneRb.velocity);
            //Debug.Break(); 
        }
    }

    public void EndLeading()
    {
        //isEndLeading = true;

        bodyRb.MovePosition(airPlane.position);
        body.parent = airPlane;
        SetActiveComponent(false);
        isEndLeading = false;
    }

    private void StopLeading()
    {
        if (isEndLeading == true && stabilizator.IsVelosityLessInaccuracy)
        {
            bodyRb.MovePosition(airPlane.position);
            body.parent = airPlane;
            SetActiveComponent(false);
            isEndLeading = false;
        }
    }

    private void SetActiveComponent(bool active)
    {
        hardKeep.enabled = active;
        stabilizator.enabled = active;
        isLeading = active;
        bodyRb.isKinematic = !active;
        bodyRb.useGravity = active;
    }
}
