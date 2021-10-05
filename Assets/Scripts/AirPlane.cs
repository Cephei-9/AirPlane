using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AirPlane : MonoBehaviour
{

    /*
     * Скрипт включает и выключает двигатель, и добавляет движущую силу самолету
     */

    public float startSpeed = 1;
    public float decreaseVelosity = 1;

    public Rigidbody airPlaneRb;

    private bool EngineActive = false;
    private float speed = 0;

    public event Action<bool> OnEngineActiveChanged;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            airPlaneRb.useGravity = EngineActive;
            EngineActive = !EngineActive;

            OnEngineActiveChanged?.Invoke(EngineActive);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (EngineActive == false)
        {
            speed = Mathf.Lerp(speed, 0, decreaseVelosity * Time.fixedDeltaTime);
        }
        else
        {
            speed = Mathf.Lerp(speed, startSpeed, decreaseVelosity * Time.fixedDeltaTime);
        }
        airPlaneRb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
    }
}
