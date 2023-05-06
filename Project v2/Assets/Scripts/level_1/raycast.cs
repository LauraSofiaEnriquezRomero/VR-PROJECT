using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast : MonoBehaviour
{
    TCPClient refTCPClient;

        // Start is called before the first frame update
    void Start()
    {
        this.refTCPClient = this.transform.GetComponent<TCPClient>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Ray mouseRay = Camera.main.ScreenPointToRay(
            new Vector3(refTCPClient.tcpX*refTCPClient.factor, refTCPClient.tcpY*refTCPClient.factor, 0));
        Vector3 direccionHechizo = mouseRay.direction;
        Debug.DrawRay(Camera.main.transform.forward, direccionHechizo * 10, Color.green);
        */

        // Lanzar objeto 
        // Camera.main.transform.forward // origen
        // Camera.main.transform.forward * 1 // direccion
    }
}


