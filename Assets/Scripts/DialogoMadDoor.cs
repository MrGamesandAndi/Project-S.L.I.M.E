using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogoMadDoor : MonoBehaviour
{
    public Animator anim;    
    public TextMeshProUGUI mostradorTexto;
    public string[] oraciones;
    private int index;
    public float velocidadEscritura;
    public GameObject botonSiguiente;
    public GameObject dialogoUI;
    public GameObject UIPrincipal;
    private bool botonActivado = false;
    public SoundManager audioManager;
    public GameObject barreraInterna;
    private MadDoorController doorController;


    private void Start()
    {
        doorController = FindObjectOfType<MadDoorController>();
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
        audioManager.PararSonido("Música");
        MoverCientifico.puedeMoverse = false;
        UIPrincipal.SetActive(false);
        dialogoUI.SetActive(true);
        foreach (char letra in oraciones[index].ToCharArray())
        {
            if(index==2)
            {
                anim.SetTrigger("Bajar");
            }
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
            mostradorTexto.text = "";
            dialogoUI.SetActive(false);
            UIPrincipal.SetActive(true);
            MoverCientifico.puedeMoverse = true;
            audioManager.ReproducirSonido("Jefe");
            Dialogo.estaHablando = false;
            doorController.PrepararBatalla();


        }
    } 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            barreraInterna.SetActive(true);
            StartCoroutine(Tipo());
            gameObject.transform.localPosition = new Vector2(gameObject.transform.position.x, 100);
        }
    }
}
