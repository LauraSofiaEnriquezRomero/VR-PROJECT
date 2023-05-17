using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectarHechizo : MonoBehaviour
{
    TCPClient refTCPClient;
    Text refTex;
    public int[] arregloCuadros = new int[5] {0,0,0,0,0};
    int idxHechizo = 0;

    // Hechizos de 5
    public static int[] expelearmus = new int[5] {2,4,3,5,1};

    // Hechizos de 3
    public static int[] adacadabra = new int[5] {2,1,5,0,0};

    void Start()
    {
        this.refTCPClient = GameObject.Find("Puntero").GetComponent<TCPClient>();
        this.refTex = this.GetComponent<Text>();
    }

    void Update()
    {   
        this.refTex.text = "Hechizo:" + this.refTCPClient.cuadro;
        patronHechizos();
    }

    public void patronHechizos() {
        // Estamos ignorando un cuadro ya leido y los cuadro 0
        if (arregloCuadros[idxHechizo] != this.refTCPClient.cuadro && this.refTCPClient.cuadro != 0) {
            idxHechizo = idxHechizo + 1;
            arregloCuadros[idxHechizo] = this.refTCPClient.cuadro;
            
            if (idxHechizo > 2){
                
                //Puede ser un hechizo de 3.
                if (idxHechizo > 4) {
                    // Es un hechizo de 5 y ya termninamos.
                    if (sonHechizosIguales(arregloCuadros, expelearmus)){
                        // Es un expelearmus
                        Debug.Log("Es un expelearmus");
                    }
                    // idxHechizo = 0;
                } else {
                    // Es un hechizo de 3
                    if (sonHechizosIguales(arregloCuadros, adacadabra)){
                        // Es un expelearmus
                        Debug.Log("Es un adacadrab");
                        // idxHechizo = 0;
                    }
                }
            }
        }
    }

    public bool sonHechizosIguales(int[] firstArray, int[] secondArray)
    {
        if (firstArray.Length != secondArray.Length)
            return false;
        for (int i = 0; i < firstArray.Length; i++)
        {
            if (firstArray[i] != secondArray[i])
                return false;
        }
        return true;
    }
}
