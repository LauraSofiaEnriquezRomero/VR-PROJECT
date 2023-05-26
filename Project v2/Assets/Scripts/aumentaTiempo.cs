using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class aumentaTiempo : MonoBehaviour
{
    public float tiempoAumentado = 2f;
    public Text tiempoRestanteText;

    public float tiempoRestante = 30f;

    public GameObject times_up;

    //DetectarHechizo detectarHechizo;

    private void start(){
        times_up.SetActive(false);
        //detectarHechizo = FindObjectOfType<DetectarHechizo>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rayo"))
        {
            tiempoRestante += tiempoAumentado;
            //detectarHechizo.AumentarTiempo(2.0f);
        }
    }

    private void Update()
    {
        tiempoRestante -= Time.deltaTime;

        if (tiempoRestante <= 0f)
        {
            tiempoRestante = 0f;

            // Debug.Log("Tiempo agotado");
            times_up.SetActive(true);
        }

        tiempoRestanteText.text = tiempoRestante.ToString("F2");

        if (Mathf.FloorToInt(Time.time / 60f) > 0)
        {
            // Aumenta la velocidad cada minuto
            Time.timeScale = Mathf.FloorToInt(Time.time / 60f) + 1;
        }

    }


}
    

