using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlane : MonoBehaviour
{
    /*
     * Скрипт занимается менеджментом других скриптов занимающихся вращением по конкретным осям. Он предоставляет им информацию об импуте
     * а также собирает с них цифровые значения вращений и сам накладывает их
     */

    public Rigidbody selfRigidBody;
    public float multiply = 1;

    public RotateToAxixWithLockAngle rotateToZ;
    public RotateToAxis rotateToY;
    public RotateToAxis rotateToX;
    public BackRotateToAxis backRotateToZ;

    private float nowTernX = 1;
    private float nowTernY = 1;

    private void OnDisable()
    {
        //rotateToX.MultiplySpeed(1/multiply);
        //rotateToY.MultiplySpeed(1 / multiply);
        //rotateToZ.MultiplySpeed(1 / multiply);
        //backRotateToZ.MultiplySpeed(1 / multiply);
        //print("Disable");        
    }

    private void Start()
    {
        //rotateToX.MultiplySpeed(multiply);
        //rotateToY.MultiplySpeed(multiply);
        //rotateToZ.MultiplySpeed(multiply);
        //backRotateToZ.MultiplySpeed(multiply);

        //rotateToZ = new RotateToAxixWithLockAngle
        //{
        //    maxReserv = 1.4f,
        //    speedReserv = 0.02f,
        //    speedRotate = 0.8f
        //};
        //backRotateToZ = new BackRotateToAxis
        //{
        //    maxReserv = 2,
        //    speedReserv = 0.02f,
        //    speedRotate = 0.3f
        //};


        //rotateToY = new RotateToAxis
        //{
        //    maxReserv = 1,
        //    speedReserv = 0.02f,
        //    speedRotate = 0.5f
        //};
        //rotateToX = new RotateToAxis
        //{
        //    maxReserv = 1.6f,
        //    speedReserv = 0.04f,
        //    speedRotate = 0.3f
        //};
    }

    private void FixedUpdate()
    {
        TransformRotate();

        //PhysicsQuatornianRotate();
    }

    private string PrintVector3(Vector3 vector)
    {
        return $"({vector.x}, {vector.y}, {vector.z})";
    }

    private void TransformRotate()
    {
        Vector3 vector = FindObjectOfType<QuaternionTest>().RotationForTrueUpVectorNum2();
        float angle = Vector3.SignedAngle(vector, transform.up, -transform.forward);

        Vector3 localRotation = new Vector3();
        Vector3 globalRotation = new Vector3();

        float sign = 1;
        if (Input.GetKey(KeyCode.Space)) sign = -1;
        //print("Sign: " + sign);

        rotateToZ.InputControle(Input.GetAxisRaw("Horizontal"));
        localRotation.z += -rotateToZ.RotateWithControlAngle(angle);

        backRotateToZ.InputControle(Input.GetAxisRaw("Horizontal"), angle);
        localRotation.z += -backRotateToZ.RotateBack();

        rotateToY.InputControle(Input.GetAxisRaw("Horizontal"));
        globalRotation.y += rotateToY.Rotate();

        rotateToX.InputControle(Input.GetAxisRaw("Vertical"));
        localRotation.x += rotateToX.Rotate();

        transform.Rotate(localRotation); // Local rotate
        transform.Rotate(globalRotation, Space.World); // Global rotate 
    }

    private void PhysicsRotate()
    {
        Vector3 vector = FindObjectOfType<QuaternionTest>().RotationForTrueUpVectorNum2();
        float angle = Vector3.SignedAngle(vector, transform.up, -transform.forward);

        Vector3 localTorque = new Vector3();
        Vector3 globalTorque = new Vector3();

        rotateToZ.InputControle(Input.GetAxisRaw("Horizontal"));
        //localTorque.z += -rotateToZ.Delta;

        backRotateToZ.InputControle(Input.GetAxisRaw("Horizontal"), angle);
        //localTorque.z += -backRotateToZ.Delta;

        rotateToY.InputControle(Input.GetAxisRaw("Horizontal"));
        rotateToY.Rotate();
        globalTorque.y += rotateToY.Delta;

        rotateToX.InputControle(Input.GetAxisRaw("Vertical")); //Input.GetAxisRaw("Vertical")
        rotateToX.Rotate();
        localTorque.x += rotateToX.Delta;

        //print("Local Torque: " + localTorque.x + 
        //    "\nrotation: " + localRotation.x);

        selfRigidBody.AddRelativeTorque(localTorque, ForceMode.VelocityChange);
        selfRigidBody.AddTorque(globalTorque, ForceMode.VelocityChange);

        //print("global: " + PrintVector3(globalTorque));
        //print("local: " + PrintVector3(localTorque));
        //print("Main rotation: " + PrintVector3(globalTorque + transform.TransformDirection(localTorque)));
        //print("angular vel: " + PrintVector3(transform.InverseTransformVector(selfRigidBody.angularVelocity)));
    }

    private void PhysicsQuatornianRotate()
    {
        Vector3 vector = FindObjectOfType<QuaternionTest>().RotationForTrueUpVectorNum2();
        float angle = Vector3.SignedAngle(vector, transform.up, -transform.forward);

        Vector3 localRotation = new Vector3();
        Vector3 globalRotation = new Vector3();

        rotateToZ.InputControle(Input.GetAxisRaw("Horizontal"));
        localRotation.z += -rotateToZ.RotateWithControlAngle(angle);

        backRotateToZ.InputControle(Input.GetAxisRaw("Horizontal"), angle);
        localRotation.z += -backRotateToZ.RotateBack();

        rotateToY.InputControle(Input.GetAxisRaw("Horizontal"));
        globalRotation.y += rotateToY.Rotate();

        rotateToX.InputControle(Input.GetAxisRaw("Vertical"));
        localRotation.x += rotateToX.Rotate();

        Quaternion global = Quaternion.Euler(transform.InverseTransformVector(globalRotation));
        Quaternion local = Quaternion.Euler(localRotation);

        //selfRigidBody.MoveRotation((transform.rotation * local) * global);


        transform.rotation *= global;
        transform.rotation *= local;
    }

    [ContextMenu("Left")]
    public void TurnLeft()
    {
        nowTernX = 1;
        nowTernY = 1;
    }

    [ContextMenu("Right")]
    public void TurnRight()
    {
        nowTernX = -1;
        nowTernY = -1;
    }

    [ContextMenu("Zero")]
    public void ZeroTurn()
    {
        nowTernX = 0;
        nowTernY = 0;
    }

    private Vector3 RotationForTrueUpVector()        
    {
        Vector3 v = Vector3.ProjectOnPlane(transform.right, Vector3.up);
        v = Vector3.ProjectOnPlane(v, transform.forward);
        v = Vector3.ProjectOnPlane(v, transform.forward);

        Debug.DrawRay(transform.position, v * 2, Color.green);
        Debug.DrawRay(transform.position, Vector3.up, Color.green * 0.6f);

        v = Vector3.ProjectOnPlane(transform.up, v);
        v = Vector3.ProjectOnPlane(v, transform.forward);

        Debug.DrawRay(transform.position, v * 2, Color.red);
        return v;
    }
}
