using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSlime : MonoBehaviour
{
    public GameObject slimeASpawnear;
    public bool dejarDeSpawnear=false;
    public float tiempoSpawn;
    public float delay;

    void Start()
    {
        InvokeRepeating("Spawnear", tiempoSpawn, delay);
    }

    public void Spawnear()
    {
        Instantiate(slimeASpawnear, transform.position, transform.rotation);
        if(dejarDeSpawnear)
        {
            CancelInvoke("Spawnear");
        }
    }
}
