using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class QuaternionTest : MonoBehaviour
{
    public float angle;
    public Transform trueVectorUp;
    public Transform planeTransform;

    public List<Vectors> vectors;

    public Transform plane;

    private void Update()
    {
        //RotationForTrueUpVector();
        angle = Vector3.SignedAngle(RotationForTrueUpVector(), transform.up, -transform.forward);

        SetPlaneAxis();

        //RotationForTrueUpVectorNum2();

        //RotationForTrueUpVectorNum3();

        //plane.transform.rotation = Quaternion.FromToRotation(Vector3.forward, planeTransform.forward);
    }

    private Vector3 RotationForTrueUpVector()
    {
        //trueVectorUp.rotation *= Quaternion.FromToRotation(trueVectorUp.up, transform.up);
        //trueVectorUp.rotation *= Quaternion.FromToRotation(trueVectorUp.right, transform.right);

        Vector3 v = Vector3.ProjectOnPlane(planeTransform.right, Vector3.up);

        SetRotationVector(3, Quaternion.FromToRotation(Vector3.up, v), Color.red * 0.6f);
        //v = Vector3.ProjectOnPlane(v, transform.forward);

        Debug.DrawRay(transform.position, v * 2, Color.green);
        Debug.DrawRay(transform.position, Vector3.up, Color.green * 0.6f);

        v = Vector3.ProjectOnPlane(planeTransform.up, v);

        v = Vector3.ProjectOnPlane(v, planeTransform.forward);
        SetRotationVector(4, Quaternion.FromToRotation(Vector3.up, v), Color.green * 0.6f);

        Debug.DrawRay(transform.position, v * 2, Color.red);
        return v;
    }

    public Vector3 RotationForTrueUpVectorNum2()
    {
        Vector3 forwOnVecUp = Vector3.ProjectOnPlane(planeTransform.forward, Vector3.up);
        float signAngleBetwinNewForAndVecForw = Vector3.SignedAngle(forwOnVecUp, Vector3.forward, Vector3.up);
        //print(signAngleBetwinNewForAndVecForw + ": Forw");
        trueVectorUp.rotation = Quaternion.identity;
        trueVectorUp.Rotate(0, -signAngleBetwinNewForAndVecForw, 0);
        Vector3 rihtVectorUp = Vector3.ProjectOnPlane(planeTransform.up, trueVectorUp.right);
        SetRotationVector(5, Quaternion.FromToRotation(Vector3.up, rihtVectorUp), Color.white);

        Debug.DrawRay(planeTransform.position, rihtVectorUp * 6, Color.green * 0.6f);
        Debug.DrawRay(planeTransform.position, planeTransform.up * 6, Color.green);

        return rihtVectorUp;
    }

    public Vector3 RotationForTrueUpVectorNum3()
    {
        Vector3 forwOnVecUp = Vector3.ProjectOnPlane(planeTransform.forward, Vector3.up);
        float signAngleBetwinNewForAndVecForw = Vector3.SignedAngle(forwOnVecUp, Vector3.forward, Vector3.up);
        float AngleBitwinNewForwAndPlaneForw = Vector3.SignedAngle(forwOnVecUp, planeTransform.forward, trueVectorUp.right);

        print(signAngleBetwinNewForAndVecForw + ": Forw");
        trueVectorUp.rotation = Quaternion.identity;
        trueVectorUp.Rotate(0, -signAngleBetwinNewForAndVecForw, 0);
        trueVectorUp.Rotate(AngleBitwinNewForwAndPlaneForw, 0, 0); 
        
        SetRotationVector(5, Quaternion.FromToRotation(Vector3.up, trueVectorUp.up), Color.white);

        return trueVectorUp.up;
    }

    public Vector3 RotationForTrueUpVectorNum4()
    {        
        trueVectorUp.rotation = Quaternion.LookRotation(planeTransform.forward);

        Debug.DrawRay(planeTransform.position, trueVectorUp.up * 6, Color.green * 0.6f);
        Debug.DrawRay(planeTransform.position, planeTransform.up * 6, Color.green);

        return trueVectorUp.up;
    }


    private void SetRotationVector(int index, Quaternion rotation, Color color)
    {
        vectors[index].vector.rotation = rotation;
        //vectors[index].vector.GetComponentInChildren<Renderer>().material.color = color;

    }

    private void SetPlaneAxis()
    {
        SetRotationVector(0, Quaternion.FromToRotation(Vector3.up, planeTransform.up), Color.green);
        SetRotationVector(1, Quaternion.FromToRotation(Vector3.up, planeTransform.right), Color.red);
        SetRotationVector(2, Quaternion.FromToRotation(Vector3.up, planeTransform.forward), Color.blue);
    }
}

[System.Serializable]
public class Vectors
{
    public string name;
    public Transform vector;
}
