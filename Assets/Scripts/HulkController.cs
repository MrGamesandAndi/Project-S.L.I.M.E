using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HulkController : MonoBehaviour
{
    public int duracion;
    public int R;
    public int G;
    public int B;
    public int alpha=1;
    private MoverCientifico cientifico;
    private VidaCientifico vida;
    private SoundManager audioManager;
    private ManejadorDelNivel manager;

    // Start is called before the first frame update
    void Start()
    {      
        manager = FindObjectOfType<ManejadorDelNivel>();
        audioManager = SoundManager.instancia;
        if (audioManager == null)
        {
            Debug.LogError("No hay un SoundManager en la escena");
        }
        vida = FindObjectOfType<VidaCientifico>();
        cientifico = FindObjectOfType<MoverCientifico>();
        cientifico.GetComponent<SpriteRenderer>().color=new Color(R,G,B,alpha);
        StartCoroutine(Poder());
    }  

    IEnumerator Poder()
    {
        int capaEnemigo = LayerMask.NameToLayer("Enemigo");
        int capaJugador = LayerMask.NameToLayer("Jugador");
        Physics2D.IgnoreLayerCollision(capaEnemigo, capaJugador, true);
        vida.sinPoder = false;
        ManejadorDelNivel.puedeSerDañado = false;
        audioManager.ReproducirSonido("PowerUp");
        yield return new WaitForSeconds(duracion);
        vida.sinPoder = true;
        ManejadorDelNivel.puedeSerDañado = true;
        cientifico.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
        audioManager.ReproducirSonido("PowerDown");
        Physics2D.IgnoreLayerCollision(capaEnemigo, capaJugador, false);
        Destroy(gameObject);
    }
}
