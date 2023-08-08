using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torreta : MonoBehaviour
{   
    public float distancia;
    public float rangoActivacion;
    public float intervaloDisparo;
    public float velocidadProyectil = 100f;
    public float coolDownProyectil;
    private bool despierto = false;
    private bool mirandoDerecha = true;
    public GameObject proyectil;
    public Transform objetivo;
    private Animator animator;
    public Transform puntoDisparoIzquierda;
    public Transform puntoDisparoDerecha;
    private SoundManager audioManager;

    private void Awake()
    {

        animator = GetComponent<Animator>();
    }

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
        animator.SetBool("Despierto",despierto);
        animator.SetBool("MirandoDerecha", mirandoDerecha);
        CheckRango();
        if(objetivo.transform.position.x>transform.position.x)
        {
            mirandoDerecha = true;
        }
        if(objetivo.transform.position.x<transform.position.x)
        {
            mirandoDerecha = false;
        }
    }

    public void CheckRango()
    {
        distancia = Vector3.Distance(transform.position, objetivo.transform.position);

        if(distancia<rangoActivacion)
        {
            despierto = true;
        }

        if(distancia>rangoActivacion)
        {
            despierto = false;
        }
    }

    public void Atacar(bool atacandoDerecha)
    {
        coolDownProyectil += Time.deltaTime;
        if(coolDownProyectil>=intervaloDisparo)
        {
            audioManager.ReproducirSonido("DisparoTorreta");

            Vector2 direccion = objetivo.transform.position - transform.position;
            direccion.Normalize();

            if (!atacandoDerecha)
            {
                GameObject clonProyectil;
                clonProyectil = Instantiate(proyectil, puntoDisparoIzquierda.transform.position, puntoDisparoIzquierda.transform.rotation) as GameObject;
                clonProyectil.GetComponent<Rigidbody2D>().velocity = direccion * velocidadProyectil;
                coolDownProyectil = 0;
            }

            if(atacandoDerecha)
            {
                GameObject clonProyectil;
                clonProyectil = Instantiate(proyectil, puntoDisparoDerecha.transform.position, puntoDisparoDerecha.transform.rotation) as GameObject;
                clonProyectil.GetComponent<Rigidbody2D>().velocity = direccion * velocidadProyectil;
                coolDownProyectil = 0;
            }
        }
    }

}
