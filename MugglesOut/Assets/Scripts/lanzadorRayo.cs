using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lanzadorRayo : MonoBehaviour
{
    public float distancia = 10f;

    void Update()
    {
        Ray rayo = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayo, out hitInfo, distancia))
        {
            if (hitInfo.collider.CompareTag("Objetivo"))
            {
                hitInfo.collider.GetComponent<aumentaTiempo>().OnCollisionEnter(null);
            }
        }
    }
}
