using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaAtaqueTorreta : MonoBehaviour
{
    public Torreta torreta;
    public bool estaIzquierda = false;

    private void Awake()
    {
        torreta = gameObject.GetComponentInParent<Torreta>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(estaIzquierda)
            {
                torreta.Atacar(false);
            }
            else
            {
                torreta.Atacar(true);
            }
        }
    }
}
