using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LluviaAcidaController : MonoBehaviour
{
    private SemillaController semillaController;
    private SoundManager audioManager;
    private ManejadorDelNivel manager;
    private Animator anim;
    public float duracion=4f;
    // Start is called before the first frame update
    void Start()
    {
        semillaController = FindObjectOfType<SemillaController>();
        manager = FindObjectOfType<ManejadorDelNivel>();
        audioManager = SoundManager.instancia;
        if (audioManager == null)
        {
            Debug.LogError("No hay un SoundManager en la escena");
        }
        StartCoroutine(TiempoVidaLluvia());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemigo")
        {
            anim = other.GetComponent<Animator>();
            manager.AnimacionMuerteSlime(anim);
        }
        if (other.tag == "Semilla")
        {
            semillaController.StartCoroutine("Crecimiento");
            
        }

        Debug.Log("toco :"+other.name);
    }

    IEnumerator TiempoVidaLluvia()
    {
        audioManager.ReproducirSonido("Lluvia");       
        yield return new WaitForSeconds(duracion);
        Destroy(gameObject);
        audioManager.PararSonido("Lluvia");
    }
}
