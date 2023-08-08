using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barreras : MonoBehaviour
{
    ManejadorDelNivel manejadorDelNivel;

    private void Start()
    {
        manejadorDelNivel = FindObjectOfType<ManejadorDelNivel>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Enemigo")
        {
            manejadorDelNivel.AnimacionMuerteCallado(collision.GetComponent<Animator>());
        }
    }
}
