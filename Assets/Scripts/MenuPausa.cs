using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    private ManejadorDelNivel manejadorDelNivel;
    public GameObject UIKeyBinds;
    public GameObject UIMenuOpciones;
    public static bool enPausa = false;
    public GameObject UIMenuPausa;
    private LevelLoaderController levelLoaderController;    
    private SoundManager audioManager;
    public string musica = "Jefe";


    private void Start()
    {
        manejadorDelNivel = FindObjectOfType<ManejadorDelNivel>();
        levelLoaderController = FindObjectOfType<LevelLoaderController>();
        audioManager = SoundManager.instancia;
        if (audioManager == null)
        {
            Debug.LogError("No hay un SoundManager en la escena");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !Dialogo.estaHablando && ManejadorDelNivel.puedePausar)
        {
            if(enPausa)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Reanudar()
    {
        audioManager.ReproducirSonido(musica);
        UIKeyBinds.SetActive(false);
        UIMenuOpciones.SetActive(false);
        UIMenuPausa.SetActive(false);
        Time.timeScale = 1f;
        enPausa = false;
    }

    void Pausar()
    {
        audioManager.PausarSonido(musica);
        UIMenuPausa.SetActive(true);
        Time.timeScale = 0f;
        enPausa = true;
    }

    public void CargarOpciones()
    {
        Debug.Log("Cargando opciones...");
        UIMenuOpciones.SetActive(true);
        UIMenuPausa.SetActive(false);
    }

    public void CargarKeyBinds()
    {
        Debug.Log("Cargando keybinds...");
        UIKeyBinds.SetActive(true);
        UIMenuPausa.SetActive(false);
    }

    public void Salir()
    {
        enPausa = false;
        audioManager.PararSonido("Música");
        levelLoaderController.CargarNivel(0);
    }

    public void SalirKeybind()
    {
        UIKeyBinds.SetActive(false);
        UIMenuPausa.SetActive(true);
        Debug.Log("Saliendo keybinds...");
    }

}
