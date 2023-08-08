using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeController : MonoBehaviour
{

    public GameObject imagenJugador;
    public GameObject imagenContrincante;
    public TextMeshProUGUI textoVs;
    // Start is called before the first frame update
    public void Iniciar()
    {
        gameObject.SetActive(true);
        imagenJugador.SetActive(true);
        imagenContrincante.SetActive(true);
        textoVs.enabled = true;
    }  
    
    public void Finalizar()
    {
        gameObject.SetActive(false);
        imagenJugador.SetActive(false);
        imagenContrincante.SetActive(false);
        textoVs.enabled = false;
    }
}
