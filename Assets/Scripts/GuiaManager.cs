using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiaManager : MonoBehaviour
{
    public GameObject botonEncendido;
    public Dialogo dialogo;
    private Animator animator;
    private bool puedeHablar = false;
    private bool yaPresiono=false;
    private int capaEnemigo;
    private int capaJugador;
    public GameObject luz;

    private void Start()
    {
        capaEnemigo = LayerMask.NameToLayer("Enemigo");
        capaJugador = LayerMask.NameToLayer("Jugador");
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(puedeHablar && MoverCientifico.puedeMoverse)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && !yaPresiono)
            {
                luz.SetActive(true);
                Physics2D.IgnoreLayerCollision(capaEnemigo, capaJugador, true);
                botonEncendido.SetActive(false);
                yaPresiono = true;
                animator.SetBool("prendido", true);
                dialogo.StartCoroutine("Tipo");
            }       
        }   
        
        if(Dialogo.acabo)
        {
            luz.SetActive(false);
            animator.SetBool("prendido", false);
            Physics2D.IgnoreLayerCollision(capaEnemigo, capaJugador, false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            yaPresiono = false;
            botonEncendido.SetActive(true);            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            puedeHablar = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            puedeHablar = false;
            animator.SetBool("prendido", false);
            botonEncendido.SetActive(false);
        }
    }
}
