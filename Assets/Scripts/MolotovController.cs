using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovController : MonoBehaviour
{
    private SoundManager audioManager;
    private ManejadorDelNivel manager;
    private Animator anim;
    private MoverCientifico mc;

    public float velocidadx;
    public float velocidady;
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
        if (mc.transform.localScale.x < 0)
        {
            velocidadx = -velocidadx;
            transform.localScale = new Vector2(-0.3f, 0.3f);
        }
        else
        {
            transform.localScale = new Vector2(0.3f, 0.3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Movimiento());          
    }

    IEnumerator Movimiento()
    {
        while (velocidady > 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadx, velocidady);
            yield return new WaitForSeconds(0.1f);
            velocidady = velocidady - 0.01f;
        }       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        audioManager.ReproducirSonido("Molotov");
        if (other.tag == "Enemigo")
        {           
            anim = other.GetComponent<Animator>();
            manager.AnimacionMuerteSlime(anim);
            Destroy(gameObject);
        }
        else
        {
            if(other.tag=="Piso")
            {
                Destroy(gameObject);
            }
        }
    }
}
