using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TecladoController : MonoBehaviour
{
    private LevelLoaderController loaderController;

    private void Start()
    {
        loaderController = FindObjectOfType<LevelLoaderController>();
    }

    public void Salir()
    {
        loaderController.CargarNivel(13);
    }
}
