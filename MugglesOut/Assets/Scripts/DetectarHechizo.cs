using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectarHechizo : MonoBehaviour
{
    TCPClient refTCPClient;
    Text refTex;
    Spell refSpell;
    
    public int contadorMonedas = 10;
    public Text monedas;
    public int totalMonedasContador;
    public Text totalMonedas;

    public int idxHechizo = 0;
    public int[] arregloCuadros = new int[6] {0,0,0,0,0,0};

    // Hechizos de 5
    public static int[] expelearmus = new int[6] {0,2,4,3,5,1};
    
    // Hechizos de 4
    public static int[] wingardium = new int[6] {0,3,2,1,4,0};

    // Hechizos de 3
    public static int[] patronus = new int[6] {0,5,1,4,0,0};
    public static int[] lumos = new int[6] {0,3,2,4,0,0};

    // Hechizos de 2
    public static int[] impedimenta = new int[6] {0,3,4,0,0,0};

    //almacena posiciones unicas
    HashSet<int> posicionesUnicas = new HashSet<int>();

    public aumentaTiempo cronometroMasDos;

    public destelloCod patronDestello;



    void Start()
    {
        this.refTCPClient = GameObject.Find("Puntero").GetComponent<TCPClient>();
        this.refSpell = GameObject.Find("launchSparkles").GetComponent<Spell>();
        this.refTex = this.GetComponent<Text>();
        cronometroMasDos = GameObject.FindObjectOfType<aumentaTiempo>();
        patronDestello = GameObject.FindObjectOfType<destelloCod>();
        //this.totalMonedas = this.GetComponent<Text>();

    }

    void Update()
    {
        this.refTex.text = "" + this.refTCPClient.cuadro;
        monedas.text = contadorMonedas.ToString();
        totalMonedasContador = contadorMonedas;
        totalMonedas.text = "Knuts recolectados: " + totalMonedasContador.ToString();
        patronHechizos();
    }
    public void patronHechizos() {

        posicionesUnicas.Add(this.refTCPClient.cuadro);

        // Estamos ignorando un cuadro ya leido y los cuadro 0
        if (arregloCuadros[idxHechizo] != this.refTCPClient.cuadro && this.refTCPClient.cuadro != 0) {
            idxHechizo++;
//            totalMonedas.Text = totalMonedasContador.ToString();

            patronDestello.cambioDestellos (idxHechizo);

            arregloCuadros[idxHechizo] = this.refTCPClient.cuadro;
            
            if (idxHechizo >= 2){

                if (idxHechizo >= 3){

                    if (idxHechizo >= 4){
                        //Puede ser un hechizo de 3.
                        if (idxHechizo >= 5) {
                            
                            // Es un hechizo de 5 y ya termninamos.
                            if (sonHechizosIguales(arregloCuadros, expelearmus)){
                                // Es un expelearmus
                                Debug.Log("Es un expelearmus");
                                this.refSpell.lanzar = true;
                                contadorMonedas= contadorMonedas+10;
                                //aumenta el tiempo
                                cronometroMasDos.masTiempo(5.0f); // Aumenta el tiempo en 2 segundos
                            }
                            //limpiar el arreglo 
                            ReiniciarArreglo();
                            idxHechizo = 0;

                        } else {

                            if (sonHechizosIguales(arregloCuadros, wingardium)){
                            // Es un wingardium
                            Debug.Log("Es un wingardium");
                            this.refSpell.lanzar = true;
                            contadorMonedas = contadorMonedas + 10;
                            // aumenta el tiempo
                            cronometroMasDos.masTiempo(5.0f); // Aumenta el tiempo en 2 segundos
                            ReiniciarArreglo();
                            idxHechizo = 0;
                            }
                         }
                    } else {
                        // Es un hechizo de 4
                        if (sonHechizosIguales(arregloCuadros, patronus) || sonHechizosIguales(arregloCuadros, lumos)){
                            // Es un expelearmus
                            Debug.Log("Es un patronus o lumos");
                            this.refSpell.lanzar = true;
                            idxHechizo = 0;
                            cronometroMasDos.masTiempo(5.0f); // Aumenta el tiempo en 2 segundos
                            //limpiar el arreglo 
                            ReiniciarArreglo();
                            contadorMonedas= contadorMonedas+10;
                        }
                    }                

                } else {
                // Es un hechizo de 4
                    if (sonHechizosIguales(arregloCuadros, impedimenta)){
                        // Es un expelearmus
                        Debug.Log("Es un impedimenta");
                        this.refSpell.lanzar = true;
                        idxHechizo = 0;
                        cronometroMasDos.masTiempo(5.0f); // Aumenta el tiempo en 2 segundos
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


    
}


