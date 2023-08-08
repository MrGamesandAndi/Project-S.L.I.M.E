using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverSlime : MonoBehaviour
{
    public float velocidad=5f;
    public bool moviendoDerecha=true;
    public Transform deteccionSuelo;
    public int distancia = 2;
    public int distanciaPared = 1;
    public bool puedeMoverse = true;
    private Vector2 direccion=Vector2.right;

    private void Start()
    {
        transform.parent = null;
    }

    private void FixedUpdate()
    {
        if(puedeMoverse)
        {
            transform.Translate(direccion * velocidad * Time.deltaTime);
            RaycastHit2D infoTierra = Physics2D.Raycast(deteccionSuelo.position, Vector2.down, distancia);
            if (infoTierra == false)
            {
                if (moviendoDerecha == true)
                {               
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    moviendoDerecha = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    moviendoDerecha = true;
                }
            }
        }
        else
        {
            transform.Translate(Vector2.right * 0 * Time.deltaTime);
        }

    }

    public void Girar(Vector3 direccionSlime, Vector2 direccion)
    {
        transform.localScale = direccionSlime;
        if (moviendoDerecha)
        {
            this.direccion = direccion;
        }
        else
        {
            this.direccion = direccion;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Plataforma Movil")
        {
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Plataforma Movil")
        {
            transform.parent = null;
        }
    }
}
