using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AumentarTiempo : MonoBehaviour
{
    public float tiempoAumentado = 2f;
    public Text tiempoRestanteText;

    private float tiempoRestante = 30f;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rayo"))
        {
            tiempoRestante += tiempoAumentado;
        }
    }

    private void Update()
    {
        tiempoRestante -= Time.deltaTime;

        if (tiempoRestante <= 0f)
        {
            tiempoRestante = 0f;
            Debug.Log("Tiempo agotado");
        }

        tiempoRestanteText.text = "Tiempo restante: " + tiempoRestante.ToString("F2");

        if (Mathf.FloorToInt(Time.time / 60f) > 0)
        {
            // Aumenta la velocidad cada minuto
            Time.timeScale = Mathf.FloorToInt(Time.time / 60f) + 1;
        }
    }
}
