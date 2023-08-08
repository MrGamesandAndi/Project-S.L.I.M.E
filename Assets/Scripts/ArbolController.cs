using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbolController : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Poder Fuego Puro")
        {
            anim.SetTrigger("Quemado");
            foreach (Collider2D item in transform.GetComponents<Collider2D>())
            {
                item.enabled = false;
            }
        }
    }
}
