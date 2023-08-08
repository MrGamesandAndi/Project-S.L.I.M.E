using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDireccion : MonoBehaviour
{
    public GameObject slime;
    private Vector3 direccionSlime;
    public MoverSlime moverSlime;

    private void Awake()
    {
        direccionSlime = slime.transform.localScale;      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 direccion;
        if(collision.tag=="Piso")
        {
            if(direccionSlime.x==1)
            {
                direccionSlime.x = -1;
                direccion = Vector2.left;
            }
            else
            {
                direccionSlime.x = 1;
                direccion = Vector2.right;
            }

            moverSlime.Girar(direccionSlime,direccion);
        }
    }
}
