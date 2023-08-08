using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTerminarNivel : MonoBehaviour
{
    public ManejadorDelNivel manejador;

    public void Activar()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            gameObject.SetActive(false);
            manejador.NivelCompletado();
        }
    }
}
