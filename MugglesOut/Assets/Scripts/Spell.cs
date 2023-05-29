using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public float velocidad = 1000.0f;
    public bool lanzar = false;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.lanzar == true)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            this.transform.position = Camera.main.transform.position;
            rb.AddForce(Camera.main.transform.forward * velocidad);
            this.lanzar = false;
            Debug.Log("Lanzando hechizo...");
        }
    }
}
