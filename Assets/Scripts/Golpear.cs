using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golpear : MonoBehaviour
{
    public bool caer=false;
    public bool alzar=false;
    public GameObject puntoInicio;
    private Rigidbody2D rb;
    private VidaCientifico vidaCientifico;
    public float velocidadSubida;
    private ManejadorDelNivel manejadorDelNivel;


    // Start is called before the first frame update
    void Start()
    {
        manejadorDelNivel = FindObjectOfType<ManejadorDelNivel>();
        rb = GetComponent<Rigidbody2D>();
        vidaCientifico = FindObjectOfType<VidaCientifico>();
    }

    // Update is called once per frame
    void Update()
    {
        if(caer)
        {
            rb.isKinematic = false;
        }

        if(alzar)
        {           
            rb.isKinematic = true;
            transform.position = Vector3.MoveTowards(transform.position, puntoInicio.transform.position, velocidadSubida);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {          
            vidaCientifico.Doler();
        }

        if (collision.tag == "Enemigo")
        {
            manejadorDelNivel.AnimacionMuerteSlime(collision.GetComponent<Animator>());
        }
    }
}
