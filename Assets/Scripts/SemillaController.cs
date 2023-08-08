using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemillaController : MonoBehaviour
{
    private SoundManager audioManager;
    private ManejadorDelNivel manager;
    public Animator anim;
    public int tiempoCrecimiento=1;
    public GameObject arbol;
    public float posicionSpawn=3.5f;
    public GameObject siguienteSemilla;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioManager = SoundManager.instancia;
        if (audioManager == null)
        {
            Debug.LogError("No hay un SoundManager en la escena");
        }
    }    

    IEnumerator Crecimiento()
    {
        anim.SetTrigger("Crecer");
        yield return new WaitForSeconds(tiempoCrecimiento);
        Instantiate(arbol, new Vector2(transform.position.x,transform.position.y+posicionSpawn),transform.rotation);
        Destroy(gameObject);
        if(siguienteSemilla!=null)
        {
            siguienteSemilla.SetActive(true);
        }
    }
}
