using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectarHechizo : MonoBehaviour
{
    TCPClient refTCPClient;
    Text refTex;

    PatronusSpell refPatronusSpell;
    
    public int contadorMonedas = 10;
    public Text monedas;

    public int idxHechizo = 0;
    public int[] arregloCuadros = new int[6] {0,0,0,0,0,0};

    // Hechizos de 5
    public static int[] expelearmus = new int[6] {0,2,4,3,5,1};

    // Hechizos de 3
    public static int[] patronus = new int[6] {0,5,1,4,0,0};

    //almacena las posiciones unicas
    HashSet<int> posicionesUnicas = new HashSet<int>();

    public aumentaTiempo refAumentaTiempo;

    //aparecer imagenes random
    public Transform pos;
    public GameObject [] instaciarImagenes; 


    void Start()
    {
        this.refTCPClient = GameObject.Find("Puntero").GetComponent<TCPClient>();
        this.refPatronusSpell = GameObject.Find("Patronus").GetComponent<PatronusSpell>();
        this.refTex = this.GetComponent<Text>();
    }

    void Update()
    {
        this.refTex.text = "Esta en :" + this.refTCPClient.cuadro;
        monedas.text = contadorMonedas.ToString();
        patronHechizos();
    }
    public void patronHechizos() {

        posicionesUnicas.Add(this.refTCPClient.cuadro);


        // Estamos ignorando un cuadro ya leido y los cuadro 0
        if (arregloCuadros[idxHechizo] != this.refTCPClient.cuadro && this.refTCPClient.cuadro != 0) {
            idxHechizo++;

            arregloCuadros[idxHechizo] = this.refTCPClient.cuadro;
            
            if (idxHechizo >= 3){
                
                //Puede ser un hechizo de 3.
                if (idxHechizo >= 5) {
                    // Es un hechizo de 5 y ya termninamos.
                    // Debug.Log("El arregloCuadros está lleno");

                    if (sonHechizosIguales(arregloCuadros, expelearmus)){
                        // Es un expelearmus
                        Debug.Log("Es un expelearmus");
                        contadorMonedas= contadorMonedas+10;
                        refAumentaTiempo.tiempoRestante += refAumentaTiempo.tiempoAumentado;                   
                    }

                    //limpiar el arreglo 
                    ReiniciarArreglo();
                    idxHechizo = 0;
                } else {
                    // Es un hechizo de 4
                    if (sonHechizosIguales(arregloCuadros, patronus)){
                        // Es un expelearmus
                        Debug.Log("Es un patronus");
                        this.refPatronusSpell.lanzar = true;
                        idxHechizo = 0;
                        //limpiar el arreglo 
                        ReiniciarArreglo();
                        contadorMonedas= contadorMonedas+10;
                    }
                }
            }
        }
    }
    private void ReiniciarArreglo(){
        idxHechizo = 0;
        arregloCuadros = new int[6];
    }

    public void imprimirArray(int []arreglo, string nombre = "") {
        string texto = "[";
        for (int i = 0; i < arreglo.Length; i++)
        {
            if (i==0){
                texto += arreglo[i];
            } else {
                texto +=  "," + arreglo[i];
            }
        }

        texto += "]";
        Debug.Log(nombre + texto);
    }

    public bool sonHechizosIguales(int[] arregloCuadros, int[] hechizoComparar)
    {
        // imprimirArray(arregloCuadros,"arregloCuadros");
        // imprimirArray(hechizoComparar,"hechizoComparar");

        if (arregloCuadros.Length != hechizoComparar.Length)
            return false;
        for (int i = 0; i < arregloCuadros.Length; i++)
        {
            if (arregloCuadros[i] != hechizoComparar[i])
                return false;
        }
        return true;
    }

    public void AumentarTiempo(float segundos)
    {
        aumentaTiempo refAumentaTiempo = FindObjectOfType<aumentaTiempo>();
        refAumentaTiempo.tiempoRestante += segundos;
    }
}


