using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogo : MonoBehaviour
{
    public static bool estaHablando=false;
    public TextMeshProUGUI mostradorTexto;
    public string[] oraciones = null;
    private int index;
    public float velocidadEscritura;
    public GameObject botonSiguiente;
    public GameObject dialogoUI;
    public GameObject UIPrincipal; 
    public static bool acabo = false;
    private bool botonActivado = false;

    

    private void Update()
    {       
        if(botonActivado)
        {
            botonSiguiente.GetComponent<Button>().interactable = true;
        }
        else
        {
            botonSiguiente.GetComponent<Button>().interactable = false;
        }
    }

    IEnumerator Tipo()
    {
        estaHablando = true;
        acabo = false;
        ManejadorDelNivel.puedeSerDañado = false;
        MoverCientifico.puedeMoverse = false;
       // UIPrincipal.SetActive(false);
        dialogoUI.SetActive(true);
        foreach (char letra in oraciones[index].ToCharArray())
        {
            mostradorTexto.text += letra;
            yield return new WaitForSeconds(velocidadEscritura);
        }
        botonActivado = true;
    }

    public void SiguienteOracion()
    {
        botonActivado = false;
        if (index<oraciones.Length-1)
        {
            index++;
            mostradorTexto.text = "";
            StartCoroutine(Tipo());
        }
        else
        {
            estaHablando = false;
            botonActivado = false;
            mostradorTexto.text = "";
            dialogoUI.SetActive(false);
            //UIPrincipal.SetActive(true);
            acabo = true;
            MoverCientifico.puedeMoverse = true;
            ManejadorDelNivel.puedeSerDañado = true;
            index = 0;
            Debug.Log("ñe");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            StartCoroutine(Tipo());
        }
    }
}
