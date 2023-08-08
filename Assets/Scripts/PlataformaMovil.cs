using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    public GameObject plataforma;
    public float velocidad;
    private Transform puntoActual;
    public Transform[] puntos;
    public int seleccionPunto;
    public bool puedeMoverse=false;
    public bool esDeUnaSolaVia = false;
    // Start is called before the first frame update
    void Start()
    {
        puntoActual = puntos[seleccionPunto];
    }

    // Update is called once per frame
    void Update()
    {
        if(puedeMoverse)
        {
            plataforma.transform.position = Vector3.MoveTowards(plataforma.transform.position, puntoActual.position, Time.deltaTime * velocidad);

            if(plataforma.transform.position==puntoActual.position)
            {
                seleccionPunto++;
                if(seleccionPunto==puntos.Length)
                {
                    if(esDeUnaSolaVia)
                    {
                        puedeMoverse = false;
                    }
                    else
                    {
                        seleccionPunto = 0;
                    }
                }
                puntoActual = puntos[seleccionPunto];
            }
        }
    }
}
