using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatarCientifico : MonoBehaviour
{
    public CinemachineVirtualCamera camara;
    private float gravedad;
    private ManejadorDelNivel manejadorDelNivel;
    private MoverCientifico cientifico;
    private SoundManager audioManager;
    private LevelLoaderController levelLoaderController;

    private void Start()
    {
        manejadorDelNivel = FindObjectOfType<ManejadorDelNivel>();
        cientifico = FindObjectOfType<MoverCientifico>();
        levelLoaderController = FindObjectOfType<LevelLoaderController>();
        audioManager = SoundManager.instancia;
        if (audioManager == null)
        {
            Debug.LogError("No hay un SoundManager en la escena");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Enemigo")
        {
            Destroy(collision.gameObject);
        }

        if(collision.tag=="Player")
        {
            camara.Follow = null;
            StartCoroutine("MatarCaida");            
        }
    }

    IEnumerator MatarCaida()
    {
        audioManager.PararSonido(manejadorDelNivel.musica);
        audioManager.ReproducirSonido("CientificoMuerte");
        MoverCientifico.puedeMoverse = false;
        manejadorDelNivel.BorrarInventario();
        cientifico.gameObject.GetComponent<Animator>().SetTrigger("muerto");
        gravedad = cientifico.gameObject.GetComponent<Rigidbody2D>().gravityScale;
        cientifico.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
        yield return new WaitForSeconds(1.5f);
        cientifico.gameObject.GetComponent<Rigidbody2D>().gravityScale = gravedad;
        levelLoaderController.CargarNivel(manejadorDelNivel.nivelACargar);
        MoverCientifico.puedeMoverse = true;

    }
}
