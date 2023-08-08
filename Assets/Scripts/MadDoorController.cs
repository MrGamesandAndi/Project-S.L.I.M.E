using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadDoorController : MonoBehaviour
{
    public GameObject luz1, luz2;
    public int vidaJefe;
    private SoundManager audioManager;
    private Animator animator;
    public float tiempoStun=1f;
    public GameObject colliders;
    public float tiempoMirando = 5f;
    private float tiempoDelta;
    private float acumulador;
    public float tiempoUniversal=5f;
    private bool empezoPelea=false;
    public int rng = 0;
    public GameObject manoIzq;
    public GameObject manoDer;
    public float tiempoAtaque=3f;
    public GameObject piedra;
    public Transform[] puntoSpawnPiedras;
    public GameObject[] slimes;
    private DialogoMadDoor dialogoMadDoor;
    private ManejadorDelNivel manejadorDelNivel;
    public Transform explosion;
    
    void Start()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        manejadorDelNivel = FindObjectOfType<ManejadorDelNivel>();
        dialogoMadDoor = FindObjectOfType<DialogoMadDoor>();
        audioManager = FindObjectOfType<SoundManager>();
        animator=GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {           

        if(empezoPelea)
        {          
            tiempoDelta = Time.deltaTime;
            acumulador = acumulador + Time.deltaTime;

            if (acumulador >= tiempoUniversal)
            {
                rng = Random.Range(1, 21);


                if (rng<=5)
               {
                    StartCoroutine(Atacar("izq"));
               }

               if(rng>5 && rng<=15)
               {
                    StartCoroutine(Atacar("der"));
                }

               if(rng>15)
               {
                    luz1.SetActive(true);
                    luz2.SetActive(true);
                    StartCoroutine("Mirar");
               }
                acumulador = 0;
            }
        }    
               
    }

    public void EstadoLuces()
    {      
        luz1.SetActive(false);
        luz2.SetActive(false);
    }

    IEnumerator Morir()
    {
        manejadorDelNivel.PuntajeJefe(300);
        luz1.SetActive(false);
        luz2.SetActive(false);
        empezoPelea = false;
        StopAllCoroutines();
        animator.ResetTrigger("Bajar");
        animator.ResetTrigger("Subir");
        animator.ResetTrigger("Mirar");
        animator.ResetTrigger("Daño_fase1");
        animator.SetTrigger("Morir");
        Destroy(manoDer);
        Destroy(manoIzq);
        Instantiate(explosion, transform.position,transform.rotation);
        audioManager.ReproducirSonido("Explosion");
        Destroy(gameObject);      
        audioManager.PararSonido("Jefe");
        colliders.SetActive(false);
        yield return new WaitForSeconds(1f);       
        manejadorDelNivel.NivelCompletado();
    }

    IEnumerator Atacar(string mano)
    {

        if (mano=="izq")
        {
            manoIzq.GetComponent<Golpear>().alzar = false;
            manoIzq.GetComponent<Golpear>().caer = true;
            yield return new WaitForSeconds(tiempoAtaque);
            manoIzq.GetComponent<Golpear>().caer = false;
            manoIzq.GetComponent<Golpear>().alzar = true;
            CamaraController.puedeAgitar = true;

        }

        if (mano == "der")
        {
            manoDer.GetComponent<Golpear>().alzar = false;
            manoDer.GetComponent<Golpear>().caer = true;
            yield return new WaitForSeconds(tiempoAtaque);
            manoDer.GetComponent<Golpear>().caer = false;
            manoDer.GetComponent<Golpear>().alzar = true;
            CamaraController.puedeAgitar = true;

        }

        for (int i = 0; i < puntoSpawnPiedras.Length; i++)
        {
            if (rng % 2 == 0)
            {
                rng = Random.Range(1, 3);
                if (rng == 1)
                {
                    Instantiate(slimes[0], puntoSpawnPiedras[i].position, puntoSpawnPiedras[i].rotation);
                }

                if (rng == 2)
                {
                    Instantiate(slimes[1], puntoSpawnPiedras[i].position, puntoSpawnPiedras[i].rotation);
                }
            }
            else
            {
                Instantiate(piedra, puntoSpawnPiedras[i].position, puntoSpawnPiedras[i].rotation);
            }
        }
    }

    IEnumerator Mirar()
    {
        yield return new WaitForSeconds(2);
        animator.SetTrigger("Bajar");
        animator.SetTrigger("Mirar");
        animator.ResetTrigger("Subir");
        gameObject.GetComponent<Collider2D>().enabled = true;
        yield return new WaitForSeconds(tiempoMirando);      
        animator.SetTrigger("Subir");
        gameObject.GetComponent<Collider2D>().enabled = false;
        animator.ResetTrigger("Mirar");
        animator.ResetTrigger("Bajar");
        luz1.SetActive(false);
        luz2.SetActive(false);
    }

    public void PrepararBatalla()
    {
        colliders.SetActive(true);         
        animator.ResetTrigger("Bajar");
        empezoPelea = true;
        animator.SetTrigger("Subir");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {    
        if(collision.tag!="Enemigo")
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            StartCoroutine("Daño");
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }

    IEnumerator Daño()
    {
        if (vidaJefe <= 0)
        {
            StartCoroutine(Morir());
        }
        else
        {
            vidaJefe--;
            animator.SetTrigger("Daño_Fase1");
            yield return new WaitForSeconds(tiempoStun);
            animator.ResetTrigger("Daño_Fase1");
            animator.SetTrigger("Subir");
        }       
    }
}
