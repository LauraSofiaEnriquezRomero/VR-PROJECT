using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class aumentaTiempo : MonoBehaviour
{
    public Text tiempoRestanteText;
    public float tiempoRestante = 40.0f;
    public GameObject times_up;

    private void Start()
    {
        times_up.SetActive(false);
    }

    private void Update()
    {
        actualizarTiempo();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rayo"))
        {
            masTiempo(10f);
        }
    }

    private void actualizarTiempo()
    {
        tiempoRestante -= Time.deltaTime;

        if (tiempoRestante <= 0f)
        {
            tiempoRestante = 0f;
            times_up.SetActive(true);
        }

        tiempoRestanteText.text = tiempoRestante.ToString("F2");
    }

    public void masTiempo(float segundos)
    {
        tiempoRestante += segundos;
    }
}
