using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraundCheck : MonoBehaviour
{
    private List<GameObject> collidersWithTryCollision = new List<GameObject>();

    public bool Graunded { get; private set; } = false;

    private void OnCollisionEnter(Collision collision)
    {
        foreach (var item in collision.contacts)
        {
            if (Vector3.Dot(item.normal, transform.up) > 0.5f)
            {
                collidersWithTryCollision.Add(collision.collider.gameObject);
                Graunded = collidersWithTryCollision.Count > 0;
                break;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        collidersWithTryCollision.Remove(collision.collider.gameObject);
        Graunded = collidersWithTryCollision.Count > 0;
    }
}
