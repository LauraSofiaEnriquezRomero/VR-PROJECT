/**
* Permite integrar processing con Unity, para leer un marcador y mover
* un objeto.
*/
using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

public class TCPClient : MonoBehaviour {

    internal Boolean socketReady = false;
    TcpClient mySocket;
    NetworkStream theStream;
    StreamReader theReader;
    //String Host = "11.11.19.43";
    String Host = "localhost";
    Int32 Port = 80;

    public int tcpX = 0;
    public int tcpY = 0;
    public int cuadro = 0;

    public float factor = 0.3f;

    public Thread mainThread;

    public bool killThread = false;

    void Start () {
        mainThread = new Thread(hiloPrincipal);
        mainThread.Start();
    }

    void hiloPrincipal() {
        abrirElSocket ();
        while(!killThread) {
            leerDatosProcessing ();
        }
    }

    void Update () {
        transform.position = new Vector3(0,0,0);
        transform.position = new Vector3(tcpX*factor,tcpY*factor,0);
    }

    void OnDestroy() {
        Debug.Log("Matando el hilo...");
        this.killThread = true;
    }


    /**
    * Leemos los datos que llegan por el socket
    * esta informacion la envia processing.
    * */
    public void leerDatosProcessing() {
        string informacion = readSocket ();
        if (informacion != "") {
            string[] partes = informacion.Split (
                new string[]{","},
                StringSplitOptions.None
            );
            Debug.Log ("X=" + partes [0] + " Y=" + partes [1] + " Cuadro=" + partes[2]);
            tcpX = Int32.Parse (partes [0]);
            tcpY = Int32.Parse (partes [1]);
            cuadro = Int32.Parse (partes [2]);
        }
    }


    /**
    * Crea el socket al puerto e Ip datos.
    * **/
    public void abrirElSocket() {
        try {
            mySocket = new TcpClient(Host, Port);
            theStream = mySocket.GetStream();
            theReader = new StreamReader(theStream);
            socketReady = true;
        }
        catch (Exception e) {
            Debug.Log("Socket error: " + e);
        }
    }

    /**
    * Lee datos del socket
    * **/
    public String readSocket() {
        if (!socketReady)
            return "";
        if (theStream.DataAvailable)
            return theReader.ReadLine();
        return "";
    }

    /**
    * Cierra el socket
    * */
    public void closeSocket() {
        if (!socketReady)
            return;
        theReader.Close();
        mySocket.Close();
        socketReady = false;
    }
}