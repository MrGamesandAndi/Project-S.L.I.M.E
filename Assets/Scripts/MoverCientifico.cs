using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoverCientifico : MonoBehaviour
{
    public int puntajeActual;
    public int nivelActual;
    public static MoverCientifico instancia;
    [Range(1, 100)]
    public float velocidadSalto;
    public float multiplicadorCaida=2.5f;
    public float multiplicadorSaltoCorto = 2f;
    Rigidbody2D rb;
    public float rapidezMovimiento=15f;
    public Transform checkearSuelo;
    public float radioSuelo;
    public LayerMask tierra;
    private bool estarEnSuelo;

    [HideInInspector]
    public Collider2D[] colliderCientifico;

    private SoundManager audioManager;

    public static bool puedeMoverse = true;

    Vector3 direccionPersonaje;
    private Animator anim;
    float movimiento;

    private void Start()
    {
        transform.parent = null;
        anim = GetComponent<Animator>();
        instancia = this;
        colliderCientifico = GetComponents<Collider2D>();
        audioManager = SoundManager.instancia;
        if (audioManager == null)
        {
            Debug.LogError("No hay un SoundManager en la escena");
        }
    }

    void Awake()
    {
        nivelActual = SceneManager.GetActiveScene().buildIndex;
        rb = GetComponent<Rigidbody2D>();
        direccionPersonaje = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (puedeMoverse)
        {
            movimiento = 0;
            if (estarEnSuelo && KeyBindingManager.GetKeyDown(KeyAction.saltar))
            {
                anim.SetBool("estaSaltando", true);
                audioManager.ReproducirSonido("Salto");
                GetComponent<Rigidbody2D>().velocity = Vector2.up * velocidadSalto;
            }

            if (KeyBindingManager.GetKey(KeyAction.left))
            {
                transform.Translate(Vector2.left * rapidezMovimiento * Time.deltaTime);
                direccionPersonaje.x = -18;
                movimiento = 1;
            }

            if (KeyBindingManager.GetKey(KeyAction.right))
            {
                transform.Translate(Vector2.right * rapidezMovimiento * Time.deltaTime);
                direccionPersonaje.x = 18;
                movimiento = 1;
            }
           
            transform.localScale = direccionPersonaje;

            

            if (movimiento == 0)
            {
                anim.SetBool("estaCorriendo", false);
            }
            else
            {
                anim.SetBool("estaCorriendo", true);
            }
        }  
        else
        {
            anim.SetBool("estaCorriendo", false);
        }
    }

    void FixedUpdate()
    {
        if(rb.velocity.y<0)
        {
            anim.SetBool("estaSaltando", false);
            rb.velocity += Vector2.up * Physics2D.gravity.y * (multiplicadorCaida - 1) * Time.deltaTime;
        }
        else if(rb.velocity.y>0 && !KeyBindingManager.GetKey(KeyAction.saltar))
        {
           
            rb.velocity += Vector2.up * Physics2D.gravity.y * (multiplicadorSaltoCorto - 1) * Time.deltaTime;
        }

        estarEnSuelo = Physics2D.OverlapCircle(checkearSuelo.position, radioSuelo, tierra);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag=="Plataforma Movil")
        {
            
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Plataforma Movil")
        {
            transform.parent = null;
        }
    }
}
