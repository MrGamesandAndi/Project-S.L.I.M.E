using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TituloController : MonoBehaviour
{
    private SoundManager audioManager;
    private LevelLoaderController loaderController;
    public GameObject botonCargar;

    private void Awake()
    {
        Time.timeScale = 1f;       
    }

    void Start()
    {
        if (SistemaGuardado.Verificar())
        {
            botonCargar.SetActive(true);
        }
        else
        {
            botonCargar.SetActive(false);
        }
        loaderController = FindObjectOfType<LevelLoaderController>();
        audioManager = SoundManager.instancia;
        if (audioManager == null)
        {
            Debug.LogError("No hay un SoundManager en la escena");
        }
        audioManager.ReproducirSonido("Titulo");       
    }

    public void IniciarNuevoJuego()
    {
        audioManager.PararSonido("Titulo");
    }

    public void SalirJuego()
    {
        Application.Quit();
    }

    public void AbrirOpciones()
    {
        loaderController.CargarNivel(13);
    }

    public void CargarJuego()
    {
        audioManager.PararSonido("Titulo");
        DatosJugador datos = SistemaGuardado.Cargar();
        loaderController.CargarNivel(datos.nivel);
    }

    public void AbrirCreditos()
    {
        audioManager.PararSonido("Titulo");
        loaderController.CargarNivel(16);
    }
}
