using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform[] backgrounds;
    private float[] escalasParallax;
    public float suavizado=1f;
    private Transform camara;
    private Vector3 posicionAnteriorCamara;

    private void Awake()
    {
        camara = Camera.main.transform;
    }
    

    void Start()
    {
        posicionAnteriorCamara = camara.position;
        escalasParallax = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            escalasParallax[i] = backgrounds[i].position.z * -1;
        }
    }

   
    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (posicionAnteriorCamara.x - camara.position.x) * escalasParallax[i];
            float posicionXObjetivoBackground = backgrounds[i].position.x + parallax;
            Vector3 posicionObjetivoBackground = new Vector3(posicionXObjetivoBackground, backgrounds[i].position.y, backgrounds[i].position.z);
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, posicionObjetivoBackground, suavizado * Time.deltaTime);
        }

        posicionAnteriorCamara = camara.position;
    }
}
