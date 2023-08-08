using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgarrarSlime : MonoBehaviour
{
    private Inventario inventario;
    public GameObject item;
    private SoundManager manager;
    private VidaCientifico vidaCientifico;

    private void Start()
    {
        manager = FindObjectOfType<SoundManager>();
        inventario = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventario>();
        for (int i = 0; i < inventario.slots.Length; i++)
        {
            inventario.estaLleno[i] = true;
        }
        vidaCientifico = FindObjectOfType<VidaCientifico>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {        
        if (other.tag == "Player" && KeyBindingManager.GetKey(KeyAction.agarrar))
        {
            if (!inventario.estaLleno[0] || !inventario.estaLleno[1])
            {
                for (int i = 0; i < inventario.slots.Length; i++)
                {
                    if (!inventario.estaLleno[i])
                    {
                        inventario.estaLleno[i] = true;
                        Instantiate(item, inventario.slots[i].transform, false);
                        if (item.name == "acido-icon")
                        {
                            inventario.slots[i].name = "acido";
                        }
                        if (item.name == "SlimeElectricoCapturado")
                        {
                            inventario.slots[i].name = "rayo";
                        }
                        if (item.name == "Fuego-icon")
                        {
                            inventario.slots[i].name = "fuego";
                        }
                        Destroy(gameObject);
                        break;
                    }
                }
            }
            else
            {
                vidaCientifico.Doler();
            }
        }               
    }
}
