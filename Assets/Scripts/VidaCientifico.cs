using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VidaCientifico : MonoBehaviour
{
    public int vida;  
    public int tiempoRespawn=1;

    public ManejadorDelNivel manejador;
    MoverCientifico controller;
    public float tiempoInvencibilidad=4f;   

    private SoundManager audioManager;
    public bool sinPoder = true;
    public Animator anim;

    private LevelLoaderController levelLoaderController;
    private int capaEnemigo, capaJugador, capaJefe;

    void Start()
    {
        capaEnemigo = LayerMask.NameToLayer("Enemigo");
        capaJugador = LayerMask.NameToLayer("Jugador");
        Physics2D.IgnoreLayerCollision(capaEnemigo, capaJefe, false);
        Physics2D.IgnoreLayerCollision(capaEnemigo, capaJugador, false);
        capaJefe = LayerMask.NameToLayer("Jefe");
        levelLoaderController = FindObjectOfType<LevelLoaderController>();
        audioManager = SoundManager.instancia;
        if (audioManager == null)
        {
            Debug.LogError("No hay un SoundManager en la escena");
        }
        controller = GetComponent(typeof(MoverCientifico)) as MoverCientifico;
        manejador = FindObjectOfType<ManejadorDelNivel>();       
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemigo" && vida > 0 && !KeyBindingManager.GetKey(KeyAction.agarrar) && ManejadorDelNivel.puedeSerDañado)
        {           
            StartCoroutine("Doler");
        }

        if(col.tag=="BigBoy")
        {
            StartCoroutine("Doler");
        }
    }

    public void QuitarMov(bool estado)
    {
        MoverCientifico.puedeMoverse = estado;
    }


    public void Doler()
    {
        Physics2D.IgnoreLayerCollision(capaEnemigo, capaJefe, true);
        Physics2D.IgnoreLayerCollision(capaEnemigo, capaJugador, true);
        vida--;           
        if (vida <= 0)
        {
            StartCoroutine(MatarCientifico());
        }
        else
        {
            TriggerHurt(tiempoInvencibilidad);
        }
    }

    IEnumerator HurtBlinker(float tiempoInvencibilidad)
    {
        anim.SetInteger("vida", vida);
        Animator animator = GetComponent<Animator>();
        animator.SetBool("dañado",true);
        audioManager.ReproducirSonido("CientificoDaño");              
        yield return new WaitForSeconds(tiempoInvencibilidad);
        animator.SetBool("dañado",false);
       Physics2D.IgnoreLayerCollision(capaEnemigo, capaJefe, false);
       Physics2D.IgnoreLayerCollision(capaEnemigo, capaJugador, false);
    }

    public void TriggerHurt(float tiempoInvencibilidad)
    {
        StartCoroutine(HurtBlinker(tiempoInvencibilidad));
    }

    public IEnumerator MatarCientifico()
    {
        manejador.QuitarPuntaje(20);
        manejador.Guardar();
        ManejadorDelNivel.puedePausar = false;
        Physics2D.IgnoreLayerCollision(capaEnemigo, capaJugador, true);
        audioManager.PararSonido(manejador.musica);
        audioManager.ReproducirSonido("CientificoMuerte");
        MoverCientifico.puedeMoverse = false;
        manejador.BorrarInventario();             
        gameObject.GetComponent<Animator>().SetTrigger("muerto");
        yield return new WaitForSeconds(tiempoRespawn);
        Physics2D.IgnoreLayerCollision(capaEnemigo, capaJugador, false);
        ManejadorDelNivel.puedePausar = true;
        levelLoaderController.CargarNivel(manejador.nivelACargar);
        MoverCientifico.puedeMoverse = true;
        anim.SetInteger("vida", 3);
    }
}
