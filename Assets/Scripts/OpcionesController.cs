using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class OpcionesController : MonoBehaviour
{
    public AudioMixer audioMixer;
    Resolution[] resoluciones;
    public TMP_Dropdown dropdownResoluciones;
    private LevelLoaderController loaderController;
    public GameObject mensajeBorrar;
    public GameObject botonBorrar;


    private void Start()
    {
        if (SistemaGuardado.Verificar())
        {
            botonBorrar.SetActive(true);
        }
        else
        {
            botonBorrar.SetActive(false);
        }
        resoluciones = Screen.resolutions;
        loaderController = FindObjectOfType<LevelLoaderController>();

        dropdownResoluciones.ClearOptions();

        List<string> opciones = new List<string>();

        int indexResolucionActual = 0;
        for (int i = 0; i < resoluciones.Length; i++)
        {
            string opcion = resoluciones[i].width + " x " + resoluciones[i].height;
            opciones.Add(opcion);

            if (resoluciones[i].width == Screen.width && resoluciones[i].height == Screen.height)
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
        loaderController.CargarNivel(0);
    }

    public void BorrarProgreso()
    {
        mensajeBorrar.SetActive(true);
    }

    public void ConfirmarBorrado(bool borrar)
    {
        if (borrar)
        {
            botonBorrar.SetActive(false);
            SistemaGuardado.Borrar();
        }
        mensajeBorrar.SetActive(false);
    }

    public void Volumen(float volumen)
    {
        audioMixer.SetFloat("volumen", volumen);
    }

    public void PantallaCompleta(bool estado)
    {
        Screen.fullScreen = estado;
    }

    public void IrAKeybinds()
    {
        loaderController.CargarNivel(14);
    }
}
