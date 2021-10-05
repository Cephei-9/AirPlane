using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 3;
    public float speedRotate = 20;
    public float jumpHight = 20;

    public Transform cameraTran;

    public Rigidbody rb;
    public GraundCheck graundCheck;
       
    void Update()
    {     
        if (Input.GetMouseButton(0))
        {
            float yRotate = Input.GetAxis("Mouse X") * speedRotate;
            float xRotate = cameraTran.localEulerAngles.x + -Input.GetAxis("Mouse Y") * speedRotate * Time.deltaTime;
            Mathf.Clamp(xRotate, -60, 60);

            cameraTran.localEulerAngles = new Vector3(xRotate, 0, 0);
            transform.Rotate(0, yRotate * Time.deltaTime, 0); 
        }

        if (Input.GetKeyDown(KeyCode.Space) && graundCheck.Graunded)
        {
            rb.AddForce(transform.up * jumpHight, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 inputVector = new Vector3(hAxis, 0, vAxis);
        Vector3 inputVectorToWorld = transform.TransformVector(inputVector);
        Vector3 moveVector = new Vector3(inputVectorToWorld.x, rb.velocity.y / speed, inputVectorToWorld.z);

        rb.velocity = moveVector * speed; 
    }
}
