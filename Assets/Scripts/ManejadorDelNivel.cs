using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManejadorDelNivel : MonoBehaviour
{
    public TextMeshProUGUI textoPuntaje;
    private MoverSlime slime;
    public GameObject checkPointActual;
    private MoverCientifico cientifico;
    public GameObject nivelCompletadoUI;
    public Animator animacionVictoria;
    private SistemaCrafteo mezclador;
    public GameObject UI;
    private Inventario inventario;
    private MoverSlime[] enemigos;
    private SoundManager audioManager;
    public float tiempoEspera = 2f;
    public static bool puedeSerDañado=true;
    private LevelLoaderController levelLoaderController;
    public int nivelACargar;
    public int puntaje=0;
    public static bool puedePausar=true;
    public string musica = "Música";

    private void Awake()
    {
        audioManager = SoundManager.instancia;
        if (audioManager == null)
        {
            Debug.LogError("No hay un SoundManager en la escena");
        }
        Time.timeScale = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        DatosJugador datos = SistemaGuardado.Cargar();
        levelLoaderController = FindObjectOfType<LevelLoaderController>();
        slime=FindObjectOfType<MoverSlime>();        
        cientifico = FindObjectOfType<MoverCientifico>();
        textoPuntaje.text = "";
        textoPuntaje.text= datos.puntos.ToString();
        Debug.Log("" + datos.puntos);
        mezclador = FindObjectOfType<SistemaCrafteo>();
        inventario = FindObjectOfType<Inventario>();
        audioManager.ReproducirSonido(musica);
        textoPuntaje.text = cientifico.puntajeActual.ToString();
    }

    public IEnumerator RespawnearJugador()
    {
        yield return new WaitForSeconds(tiempoEspera);
        levelLoaderController.CargarNivel(nivelACargar);
        MoverCientifico.puedeMoverse = true;
    }

    public void BorrarInventario()
    {
        if (inventario.estaLleno[0])
        {
            inventario.slots[0].name = "Slot(0)";
            Destroy(inventario.slots[0].transform.GetChild(0).gameObject);
        }
        if (inventario.estaLleno[1])
        {
            inventario.slots[1].name = "Slot(1)";
            Destroy(inventario.slots[1].transform.GetChild(0).gameObject);
        }
    }

    public void NivelCompletado()
    {
        BorrarInventario();
        audioManager.PararSonido(musica);
        audioManager.ReproducirSonido("NivelCompletado");      
        animacionVictoria.SetBool("terminoNivel",true);
        UI.SetActive(false);
        MoverCientifico.puedeMoverse = false;
        Debug.Log("Nivel completado");
        nivelCompletadoUI.SetActive(true);
        StartCoroutine("MatarTodo");
        StartCoroutine(PasarNivel());
    }

    IEnumerator PasarNivel()
    {       
        Guardar();
        yield return new WaitForSeconds(tiempoEspera);
        levelLoaderController.CargarNivel(SceneManager.GetActiveScene().buildIndex + 1);
        MoverCientifico.puedeMoverse = true;
    }

    IEnumerator MatarTodo()
    {
        enemigos = FindObjectsOfType<MoverSlime>();
        foreach (MoverSlime enemy in enemigos)
        {
            enemy.puedeMoverse = false;
            AnimacionMuerteSlime(enemy.GetComponent<Animator>());
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void AnimacionMuerteSlime(Animator enemigo)
    {
        StartCoroutine("AnimacionMuerte",enemigo);
    }

    IEnumerator AnimacionMuerte(Animator enemigo)
    {
        PuntajeSlimes(10);
        enemigo.GetComponent<MoverSlime>().puedeMoverse = false;
        audioManager.ReproducirSonido("SlimeMuerte");
        enemigo.SetBool("muerto",true);
        Physics2D.IgnoreLayerCollision(9, 10, true);
        yield return new WaitForSeconds(0.6f);        
        Physics2D.IgnoreLayerCollision(9, 10, false);
        Destroy(enemigo.gameObject);
    }

    public IEnumerator AnimacionMuerteCallado(Animator enemigo)
    {
        enemigo.GetComponent<MoverSlime>().puedeMoverse = false;
        enemigo.SetBool("muerto", true);
        Physics2D.IgnoreLayerCollision(9, 10, true);
        yield return new WaitForSeconds(0.6f);
        Physics2D.IgnoreLayerCollision(9, 10, false);
        Destroy(enemigo.gameObject);
    }

    public void Guardar()
    {
        cientifico.puntajeActual = puntaje;
        SistemaGuardado.Guardar(cientifico);
    }

    public void Puntaje(int valor)
    {
        audioManager.ReproducirSonido("Atomo");
        puntaje = puntaje + valor;
        textoPuntaje.text = "";
        textoPuntaje.text = puntaje.ToString();
    }

    public void PuntajeSlimes(int valor)
    {
        puntaje = puntaje + valor;
        textoPuntaje.text = "";
        textoPuntaje.text = puntaje.ToString();
    }

    public void PuntajeJefe(int valor)
    {
        puntaje = puntaje + valor;
        textoPuntaje.text = "";
        textoPuntaje.text = puntaje.ToString();
    }

    public void QuitarPuntaje(int valor)
    {
        puntaje = puntaje - valor;
        textoPuntaje.text = "";
        textoPuntaje.text = puntaje.ToString();
    }
}
