using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayoController : MonoBehaviour
{  
    public float duracion;
    private SoundManager audioManager;
    private ManejadorDelNivel manager;
    private Animator anim;
    private MotorController motor;


    // Start is called before the first frame update
    void Start()
    {
        motor = FindObjectOfType<MotorController>();
        manager = FindObjectOfType<ManejadorDelNivel>();
        audioManager = SoundManager.instancia;
        if (audioManager == null)
        {
            Debug.LogError("No hay un SoundManager en la escena");
        }
        StartCoroutine(TiempoVidaRayo());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemigo")
        {
            anim = other.GetComponent<Animator>();
            manager.AnimacionMuerteSlime(anim);            
        }

        if(other.tag=="Motor")
        {
            Debug.Log("mototr");
            motor.Prender();
        }
    }

    IEnumerator TiempoVidaRayo()
    {
        audioManager.ReproducirSonido("Rayo");

        yield return new WaitForSeconds(duracion);
        Destroy(gameObject);
    }
}
