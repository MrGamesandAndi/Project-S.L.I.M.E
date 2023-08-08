using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    public Sprite puertaFase1;
    public Sprite puertaFase2;
    public Sprite puertaFase3;
    public GameObject cara;
    public int cont=0;
    public GameObject madDoor;
    private Dialogo dialogo;
    private SoundManager audioManager;
    private LevelLoaderController levelLoaderController;

    private void Start()
    {
        levelLoaderController = FindObjectOfType<LevelLoaderController>();
        audioManager = FindObjectOfType<SoundManager>();
        dialogo = FindObjectOfType<Dialogo>();
        dialogo.StartCoroutine("Tipo");
    }

    public void AvanzarCutscene()
    {
        cont++;
        switch(cont)
        {
            case 1:
                madDoor.GetComponent<SpriteRenderer>().sprite = puertaFase1;
                break;
            case 3:
                madDoor.GetComponent<SpriteRenderer>().sprite = puertaFase2;
                break;
            case 6:
                Destroy(cara);
                audioManager.ReproducirSonido("Explosion");
                madDoor.GetComponent<SpriteRenderer>().sprite = puertaFase3;
                break;
            case 7:
                levelLoaderController.CargarNivel(SceneManager.GetActiveScene().buildIndex+1);
                break;
        }
    }
}
