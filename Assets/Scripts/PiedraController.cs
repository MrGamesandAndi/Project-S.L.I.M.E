using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiedraController : MonoBehaviour
{
    private VidaCientifico vidaCientifico;
    // Start is called before the first frame update
    void Start()
    {
        vidaCientifico = FindObjectOfType<VidaCientifico>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            Destroy(gameObject);
            vidaCientifico.Doler();
        }

        if(collision.tag=="Piso")
        {                     
            Destroy(gameObject);
        }
    }
}
