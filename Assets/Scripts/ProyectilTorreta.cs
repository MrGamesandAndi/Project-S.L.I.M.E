﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilTorreta : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Piso"))
        {
            Destroy(gameObject);
        }      
    }
}
