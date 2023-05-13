using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectarHechizo : MonoBehaviour
{
    TCPClient refTCPClient;
    Text refTex;
    // Start is called before the first frame update
    void Start()
    {
        this.refTCPClient = GameObject.Find("Puntero").GetComponent<TCPClient>();
        this.refTex = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        this.refTex.text = "Hechizo:" + this.refTCPClient.cuadro;
    }
}
