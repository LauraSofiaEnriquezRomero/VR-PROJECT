using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pausa : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Image imagenMute;

    public GameObject menuPausa;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = slider.value;
        RevisarMute();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSlider(float valor){
        slider.value = valor;
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        AudioListener.volume = slider.value;
        RevisarMute();
    }

    public void RevisarMute(){
        if(sliderValue == 0){
            imagenMute.enabled = true;
        }
        imagenMute.enabled = false;
    }

    public void btnPausa(){
        menuPausa.SetActive(true);
        Time.timeScale = 0;
    }

    public void volver(){
        menuPausa.SetActive(false);
        Time.timeScale = 1;
    }

    public void cerrar(){
        Debug.Log("C cerró");
        Application.Quit();
    }
}
