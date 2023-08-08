using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaFuegoController : MonoBehaviour
{
    public float velocidad;
    private SoundManager audioManager;
    private MoverCientifico mc;
    private ManejadorDelNivel manager;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<ManejadorDelNivel>();
        audioManager = SoundManager.instancia;
        if (audioManager == null)
        {
            Debug.LogError("No hay un SoundManager en la escena");
        }
        mc = FindObjectOfType<MoverCientifico>();
        audioManager.ReproducirSonido("BolaDeFuego");
        if (mc.transform.localScale.x<0)
        {
            velocidad = -velocidad;
            transform.localScale = new Vector2(-3, 3);
        }
        else
        {
            transform.localScale = new Vector2(3, 3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(velocidad, GetComponent<Rigidbody2D>().velocity.y);        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemigo")
        {
            anim = other.GetComponent<Animator>();
            if(anim!=null)
            {
                manager.AnimacionMuerteSlime(anim);
            }
            Destroy(gameObject);
        }       
    }
}
