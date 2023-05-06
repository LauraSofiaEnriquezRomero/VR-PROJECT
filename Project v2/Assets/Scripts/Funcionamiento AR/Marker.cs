using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System.Text;

public class Marker : MonoBehaviour {

    public string host = "190.99.198.250"; // direccion IP de la computadora que ejecuta Processing
    public int port = 5204; // puerto utilizado por Processing
    private TcpListener listener;
    private TcpClient client;
    private StreamReader reader;

    public GameObject objectToMove; // objeto a mover en la escena
    public float moveSpeed = 1f; // velocidad de movimiento del objeto

    private bool markerInBox = false; // variable para almacenar si el marcador ha pasado por la caja

    void Start() {
        listener = new TcpListener(System.Net.IPAddress.Parse(host), port);
        listener.Start();
        client = listener.AcceptTcpClient();
        NetworkStream ns = client.GetStream();
        reader = new StreamReader(ns, Encoding.ASCII);
    }

    void Update() {
        if (reader != null && reader.BaseStream.CanRead) {
            string message = reader.ReadLine(); // lee la variable booleana enviada desde Processing
            if (message == "1") {
                markerInBox = true; // si la variable es "1", establece la variable en verdadero
            } else {
                markerInBox = false; // si la variable es "0", establece la variable en falso
            }
        }
        if (markerInBox) {
            objectToMove.transform.position += Vector3.right * moveSpeed * Time.deltaTime; // mueve el objeto hacia la derecha si el marcador ha pasado por la caja
        }
    }

    void OnApplicationQuit() {
        if (reader != null) {
            reader.Close();
        }
        if (client != null) {
            client.Close();
        }
        if (listener != null) {
            listener.Stop();
        }
    }
}
