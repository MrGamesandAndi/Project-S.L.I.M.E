using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomoController : MonoBehaviour
{
    public int valor = 100;
    private Animator animator;
    private ManejadorDelNivel manejadorDelNivel;

    private void Start()
    {
        animator = GetComponent<Animator>();
        manejadorDelNivel = FindObjectOfType<ManejadorDelNivel>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            animator.SetTrigger("Agarrado");
            manejadorDelNivel.Puntaje(valor);
        }
    }

    public void Destruir()
    {
        Destroy(gameObject);
    }
}
