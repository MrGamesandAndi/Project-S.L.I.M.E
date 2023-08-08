using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditosController : MonoBehaviour
{
    private SoundManager audioManager;
    private LevelLoaderController loaderController;
    public string musica = "Creditos";

    void Start()
    {
        loaderController = FindObjectOfType<LevelLoaderController>();
        audioManager = SoundManager.instancia;
        if (audioManager == null)
        {
            Debug.LogError("No hay un SoundManager en la escena");
        }
        audioManager.ReproducirSonido(musica);
    }

    public void SalirCreditos()
    {
        audioManager.PararSonido(musica);
        loaderController.CargarNivel(0);
    }
}
