using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogoMuerteMadDoor : MonoBehaviour
{
    public Animator anim;
    public TextMeshProUGUI mostradorTexto;
    public string[] oraciones;
    private int index;
    public float velocidadEscritura;
    public GameObject botonSiguiente;
    public GameObject dialogoUI;
    public GameObject UIPrincipal;
    public SoundManager audioManager;
    private bool botonActivado = false;
    public GameObject barrera;


    private void Start()
    {
        audioManager = SoundManager.instancia;
        if (audioManager == null)
        {
            Debug.LogError("No hay un SoundManager en la escena");
        }
    }

    private void Update()
    {
        if (botonActivado)
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
        Dialogo.estaHablando = true;
        MoverCientifico.puedeMoverse = false;
        UIPrincipal.SetActive(false);
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
        if (index < oraciones.Length - 1)
        {
            index++;
            mostradorTexto.text = "";
            StartCoroutine(Tipo());
        }
        else
        {
            Dialogo.estaHablando = false;
            mostradorTexto.text = "";
            dialogoUI.SetActive(false);
            UIPrincipal.SetActive(true);
            MoverCientifico.puedeMoverse = true;          
        }
    }  
}
