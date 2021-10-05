using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetecter : MonoBehaviour
{
    public event Action<Collision> OnCollision;

    private void OnCollisionEnter(Collision collision)
    {
        OnCollision?.Invoke(collision);
    }
}
