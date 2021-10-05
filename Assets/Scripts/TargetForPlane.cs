using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetForPlane : MonoBehaviour
{
    /*
     * Вокруг точки на определенном расстоянии создаются шары, и когда самолет входит в тригер этого обьекта 
     * у следующего таргета запускается этот же метод
     */

    public float radius;
    public float timeCreateSpher;
    public float countSpher;

    public GameObject spherePrefab;

    public TargetForPlane nextTarget;

    private void Start()
    {
        StartCoroutine(AddLightToSpher());
    }

    public void On()
    {
        StartCoroutine(AddLightToSpher());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.gameObject.GetComponent<AirPlane>())
        {
            nextTarget?.On(); 
        }
    }

    private IEnumerator AddLightToSpher()
    {
        Transform newTransform = new GameObject().transform;
        newTransform.rotation = transform.rotation * Quaternion.Euler(0, 180, 0);
        float angle = 360 / countSpher;

        for (int i = 0; i < countSpher; i++)
        {
            Vector3 spawnPosition = transform.position + newTransform.up * radius;
            Instantiate(spherePrefab, spawnPosition, Quaternion.identity, transform.GetChild(0));
            newTransform.Rotate(0, 0, angle);
            yield return new WaitForSeconds(timeCreateSpher);
        }
    }
}
