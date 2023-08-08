using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teletransportador : MonoBehaviour
{
    private MoverCientifico cientifico;
    public GameObject destino;
    private float tiempoEspera;
    public CinemachineVirtualCamera camara;
    public float cooldown = 1.5f;

    private void Start()
    {
        cientifico = FindObjectOfType<MoverCientifico>();
        tiempoEspera = cooldown;
    }

    IEnumerator Esperar()
    {
       while(cooldown > 0)
        {
            yield return new WaitForSeconds(2);
            cooldown--;
        }
        cooldown = tiempoEspera;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag=="Player" && Input.GetKeyDown(KeyCode.UpArrow))
        {
            destino.GetComponent<Animator>().SetTrigger("Abierto");
            cientifico.transform.position = new Vector3(destino.transform.position.x, destino.transform.position.y, destino.transform.position.z);
            camara.UpdateCameraState(cientifico.transform.position, -0.1f);
            destino.GetComponent<Animator>().SetTrigger("Cerrado");
            StartCoroutine(Esperar());                       
        }
    }
}
