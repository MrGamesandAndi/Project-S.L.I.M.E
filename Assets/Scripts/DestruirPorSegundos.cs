using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirPorSegundos : MonoBehaviour
{
    public float tiempoParaSerDestruido = 0.3f;

    void Start()
    {
        Destroy(this.gameObject, tiempoParaSerDestruido);
    }
}
