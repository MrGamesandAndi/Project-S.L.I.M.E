using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamaraController : MonoBehaviour
{
    public CinemachineVirtualCamera camara;
    public float fuerza = 1f;
    public float duracion = 1f;
    public float tiempoCoolDown = 1f;
    public static bool puedeAgitar = false;
    
    private float duracionInicial;

    private void Start()
    {
        duracionInicial = duracion;
    }

    void Update()
    {
       if(puedeAgitar)
       {
            if(duracion>0)
            {
                camara.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = fuerza;
                duracion -= Time.deltaTime * tiempoCoolDown;
            }
            else
            {
                puedeAgitar = false;
                camara.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
                duracion = duracionInicial;
            }
        }      
    }
}
