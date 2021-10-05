using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stabilizator : MonoBehaviour
{
    public float distanceAmortizate = 1;
    public float power;
    public float friction = 0.5f;
    [Space]
    public float inaccuracy = 0.5f;
    [Space]
    public Rigidbody planeRb;

    public bool IsVelosityLessInaccuracy { get => verticalVelosity.magnitude < inaccuracy; }

    private Vector3 verticalVelosity;

    void FixedUpdate()
    {
        Amortizate();
    }

    public void SetVelosity(Vector3 velosity)
    {
        planeRb.velocity = velosity;
    }

    private void Amortizate()
    {
        float distanceToBody = (planeRb.transform.position - transform.position).magnitude;
        float distance = distanceToBody - distanceAmortizate;
        float distanceToFormuls = Mathf.Sign(distance) * Mathf.Abs(distance) * Mathf.Abs(distance) * 20;

        //print(distanceToFormuls);

        Vector3 newForce = -transform.up * distance * power;
        Debug.DrawRay(planeRb.transform.position, newForce, Color.red);

        planeRb.AddForce(newForce, ForceMode.VelocityChange);

        verticalVelosity = Vector3.Project(planeRb.velocity, transform.up);
        planeRb.AddForce(-verticalVelosity * friction, ForceMode.VelocityChange);
    }

    private void KeepOnMaxLengh(float distance)
    {
        if (Mathf.Abs(distance) > distanceAmortizate)
        {
            planeRb.MovePosition(transform.position + transform.up * (Mathf.Abs(distance) * distanceAmortizate));
        }

        // Or

        if (Mathf.Abs(distance) > distanceAmortizate)
        {
            // Запоминать скорость до хард кипа 

            //или просто создать мега фрикшен на этих экстра значениях и если он уж дойдет до конца тогда уже зеркалить велосити
        }
    }
    
    private void Work()
    {
        Vector3 addForcePosition = new Vector3();
        float distance = 0;
        if (!GetDistanse(ref addForcePosition, ref distance)) return;

        distance--;
        print(distance);
        Vector3 newForce = -transform.up * (distance) * power;
        //if (distance < 0)
        //{
        //    // Хуйня полная
        //    newForce = transform.up * (distance) * power;
        //    //planeRb.AddForceAtPosition(-Vector3.up * (distance) * power, addForcePosition, ForceMode.VelocityChange);
        //}
        //else
        //{
        //    newForce = -Vector3.up * (distance) * power / 2;
        //}
        planeRb.AddForceAtPosition(newForce, addForcePosition, ForceMode.VelocityChange);

        Debug.DrawRay(planeRb.transform.position, newForce);

        Vector3 planeVelosityVertical = Vector3.Project(planeRb.velocity, transform.up);
        planeRb.AddForce(-planeVelosityVertical * friction, ForceMode.VelocityChange);
    }

    private bool GetDistanse(ref Vector3 rayPoint, ref float distance)
    {
        Ray toPlane = new Ray(transform.position, transform.up);
        RaycastHit raycastHit;
        Physics.Raycast(toPlane, out raycastHit);
        if (raycastHit.collider == null) return false;

        rayPoint = raycastHit.point;
        distance = raycastHit.distance;
        return true;
    }
}

