using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoaderController : MonoBehaviour
{
    public GameObject pantallCarga;
    public Slider slider;

    public void CargarNivel(int index)
    {
        StartCoroutine(Cargar(index));
    }

    IEnumerator Cargar(int index)
    {
        AsyncOperation operacion = SceneManager.LoadSceneAsync(index);
        pantallCarga.SetActive(true);
        while(!operacion.isDone)
        {
            yield return null;
        }
    }

    public void CargarNivelPorNombre(string nombre)
    {
        StartCoroutine(CargarPorNombre(nombre));
    }

    IEnumerator CargarPorNombre(string nombre)
    {
        AsyncOperation operacion = SceneManager.LoadSceneAsync(nombre);
        pantallCarga.SetActive(true);
        while (!operacion.isDone)
        {           
            yield return null;
        }
    }
}
