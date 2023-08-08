using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechaniel : MonoBehaviour
{
    public float vida = 9;
    public Transform[] puntos;
    public float velocidad = 1f;
    public int numeroProyectilesDisparados = 6;
    public GameObject proyectil;
    public float velocidadProyectil = 5f;
    public float coolDown = 1f;
    private GameObject jugador;
    private Vector3 posicionJugador;
    public float tiempoCaida = 1f;
    public bool esVulnerable = false;
    public float tiempoVulnerable = 10f;
    private bool muerto;
    public GameObject explosion;
    private SoundManager audioManager;
    public Transform[] puntosDisparo;
    private LevelLoaderController levelLoaderController;

    void Start()
    {
        levelLoaderController = FindObjectOfType<LevelLoaderController>();
        audioManager = SoundManager.instancia;
        if (audioManager == null)
        {
            Debug.LogError("No hay un SoundManager en la escena");
        }
        jugador = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("Jefe");
    }

    private void Update()
    {
        if(vida<=0 && !muerto)
        {
            muerto = true;
            GetComponent<SpriteRenderer>().color = Color.gray;
            audioManager.PararSonido("VsSimi");
            StopCoroutine("Jefe");
            StartCoroutine("Explosiones");
        }
    }

    IEnumerator Explosiones()
    {
        audioManager.ReproducirSonido("MuerteMechaniel");
        for (int i = 0; i < 4; i++)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(.5f);
        }
        levelLoaderController.CargarNivelPorNombre("EndCutscene");
    }

    IEnumerator Jefe()
    {
        while(true)
        {
            //Primer ataque
            while (transform.position.x != puntos[0].position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(puntos[0].position.x, transform.position.y), velocidad * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(1f);

            int cont = 0;
            while (cont < numeroProyectilesDisparados)
            {
                audioManager.ReproducirSonido("ProyectilMechaniel");
                GameObject bala = Instantiate(proyectil, puntosDisparo[Random.Range(0, 2)].position, Quaternion.identity);
                bala.GetComponent<Rigidbody2D>().velocity = Vector2.left * velocidadProyectil;
                cont++;
                yield return new WaitForSeconds(coolDown);
            }

            //Segundo ataque
            GetComponent<Rigidbody2D>().isKinematic = true;
            while (transform.position != puntos[2].position)
            {
                transform.position = Vector2.MoveTowards(transform.position, puntos[2].position, velocidad * Time.deltaTime);
                yield return null;
            }

            posicionJugador = jugador.transform.position;
            yield return new WaitForSeconds(tiempoCaida);
            GetComponent<Rigidbody2D>().isKinematic = false;
            while (transform.position.x != posicionJugador.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(posicionJugador.x, transform.position.y), velocidad * Time.deltaTime);
                yield return null;
            }
            esVulnerable = true;
            audioManager.ReproducirSonido("MechanielSuelo");

            yield return new WaitForSeconds(tiempoVulnerable);
            esVulnerable = false;


            //Tercer ataque
            Transform temp;
            if (transform.position.x > jugador.transform.position.x)
            {
                temp = puntos[0];
            }
            else
            {
                temp = puntos[1];
            }

            while (transform.position.x != temp.position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(temp.position.x, transform.position.y), velocidad * Time.deltaTime);
                yield return null;
            }

            //Cuarto ataque
            if(temp==puntos[1])
            {
                transform.localScale = new Vector2(-1, 1);
                cont = 0;
                while (cont < numeroProyectilesDisparados)
                {
                    audioManager.ReproducirSonido("ProyectilMechaniel");
                    GameObject bala = Instantiate(proyectil, puntosDisparo[Random.Range(0, 2)].position, Quaternion.identity);
                    bala.GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidadProyectil;
                    cont++;
                    yield return new WaitForSeconds(coolDown);
                }
                transform.localScale = new Vector2(1, 1);
                GetComponent<Rigidbody2D>().isKinematic = true;
                while (transform.position != puntos[2].position)
                {
                    transform.position = Vector2.MoveTowards(transform.position, puntos[2].position, velocidad * Time.deltaTime);
                    yield return null;
                }              
                GetComponent<Rigidbody2D>().isKinematic = false;
            }
            else
            {
                yield return null;
            }
            
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Poder Fuego Puro" && esVulnerable)
        {
            Destroy(collision.gameObject);
            vida--;
            audioManager.ReproducirSonido("MechanielSuelo");
        }
    }
}

