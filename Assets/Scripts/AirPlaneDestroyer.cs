using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlaneDestroyer : MonoBehaviour
{
    public List<GameObject> detals;
    public List<Collider> toOffColliders;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Terrain>())
        {
            //onDie.Invoke();
            DestroyPlane();
        }
    }

    private void DestroyPlane()
    {
        DestroyOnDetals();
        OffColliders();

        GetComponent<AirPlane>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void OffColliders()
    {
        foreach (var item in toOffColliders)
        {
            item.enabled = false;
        }
    }

    private void DestroyOnDetals()
    {
        foreach (var item in detals)
        {
            item.GetComponent<Rigidbody>().isKinematic = false;
            item.GetComponent<Rigidbody>().useGravity = true;
            item.AddComponent<BoxCollider>();
            item.transform.parent = null;
            if (item.TryGetComponent<Propeler>(out Propeler propeler))
            {
                propeler.enabled = false;
            }
        }
    }
}
