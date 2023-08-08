using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public float duracion;
    private SoundManager audioManager;
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
        StartCoroutine(TiempoVidaExplosion());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemigo")
        {
            anim = other.GetComponent<Animator>();
            manager.AnimacionMuerteSlime(anim);
            
        }
    }

    IEnumerator TiempoVidaExplosion()
    {
        audioManager.ReproducirSonido("Explosion");
        yield return new WaitForSeconds(duracion);
        Destroy(gameObject);
    }
}
