using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;


public class MenuOpciones : MonoBehaviour
{
    public GameObject UIMenuPausa;
    public GameObject UIMenuOpciones;
    public AudioMixer audioMixer;
    Resolution[] resoluciones;
    public TMP_Dropdown dropdownResoluciones;

    private void Start()
    {      
        resoluciones = Screen.resolutions;

        dropdownResoluciones.ClearOptions();

        List<string> opciones = new List<string>();

        int indexResolucionActual = 0;
        for (int i = 0; i < resoluciones.Length; i++)
        {
            string opcion = resoluciones[i].width + " x " + resoluciones[i].height;
            opciones.Add(opcion);

            if(resoluciones[i].width==Screen.width && resoluciones[i].height==Screen.height)
            {
                indexResolucionActual = i;
            }
        }

        dropdownResoluciones.AddOptions(opciones);
        dropdownResoluciones.value = indexResolucionActual;
        dropdownResoluciones.RefreshShownValue();
    }

    public void Resolution(int indexResolucion)
    {
        Resolution resolucion = resoluciones[indexResolucion];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }

    public void SalirOpciones()
    {     
        UIMenuOpciones.SetActive(false);
        UIMenuPausa.SetActive(true);
        Debug.Log("Saliendo opciones...");
    }

    public void Volumen(float volumen)
    {
        audioMixer.SetFloat("volumen", volumen);
    }

    public void PantallaCompleta(bool estado)
    {
        Screen.fullScreen = estado;
    }
}
