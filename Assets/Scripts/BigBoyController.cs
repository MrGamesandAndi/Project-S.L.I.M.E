using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BigBoyController : MonoBehaviour
{
    public GameObject explosion;
    public GameObject fin;
    public float velocidad = 3f;
    public bool moviendoDerecha = true;
    public Transform deteccionSuelo;
    public int distancia = 2;
    public int distanciaPared = 1;
    public bool puedeMoverse = true;
    private Vector2 direccion = Vector2.right;
    public int vida=4;
    public GameObject jugador;
    private Animator animator;
    public float fuerzaSalto = 5f;
    private ManejadorDelNivel manejadorDelNivel;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        manejadorDelNivel = FindObjectOfType<ManejadorDelNivel>();
        fin.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (puedeMoverse)
        {
            transform.Translate(direccion * velocidad * Time.deltaTime);
            RaycastHit2D infoTierra = Physics2D.Raycast(deteccionSuelo.position, Vector2.down, distancia);
            if (infoTierra == false)
            {
                if (moviendoDerecha == true)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    moviendoDerecha = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    moviendoDerecha = true;
                }
            }
        }
        else
        {
            transform.Translate(Vector2.right * 0 * Time.deltaTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if(collision.tag=="Poder Fuego Puro" || collision.tag == "Molotov")
        {
            animator.SetTrigger("Daño");
            Destroy(collision.gameObject);
            gameObject.GetComponent<Collider2D>().enabled = false;
            vida--;
            if(vida<=0)
            {
                manejadorDelNivel.PuntajeJefe(300);
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
                fin.SetActive(true);
            }
            else
            {
                if (vida <= 2)
                {
                    animator.SetTrigger("Idle2");
                    animator.SetTrigger("Fase2");
                    velocidad = velocidad * 2;
                }
                else
                {
                    animator.SetTrigger("Idle");
                }
            }
            gameObject.GetComponent<Collider2D>().enabled = true;
        }

        if(collision.tag=="Enemigo")
        {
            Destroy(collision.gameObject);
        }
    }
}
