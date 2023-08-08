using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sonido
{
    public string nombre;
    public AudioClip clip;
    public bool loop = false;    
    
    [Range(0f,1f)]
    public float volumen;

    private AudioSource origen;

    public void SetOrigen(AudioSource origen)
    {
        this.origen = origen;
        origen.clip = clip;      
    }

    public void Play()
    {
        origen.volume = volumen;
        origen.Play();
        origen.loop = loop;
    }   

    public void Stop()
    {
        origen.Stop();
    }

    public void Pause()
    {
        origen.Pause();
    }
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instancia;

    [SerializeField]
    Sonido[] sonidos;

    void Awake()
    {
        if (instancia != null)
        {
            if (instancia != this)
            {
                Destroy(this.gameObject);
            }
           // Debug.LogWarning("Hay mas de un Audio manager en la escena");
        }
        else
        {
            instancia = this;
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {       
        for (int i = 0; i < sonidos.Length; i++)
        {
            GameObject go = new GameObject("Sonido_" + i + "_" + sonidos[i].nombre);
            go.transform.SetParent(this.transform);
            sonidos[i].SetOrigen(go.AddComponent<AudioSource>());

        }        
    }

    public void ReproducirSonido(string nombre)
    {
        for (int i = 0; i < sonidos.Length; i++)
        {
            if(sonidos[i].nombre==nombre)
            {
                Debug.Log("Tocando:" + nombre);
                sonidos[i].Play();
                return;
            }
        }
        Debug.LogWarning("No hay ese sonido en la lista");
    }

    public void PararSonido(string nombre)
    {
        for (int i = 0; i < sonidos.Length; i++)
        {
            if (sonidos[i].nombre == nombre)
            {
                sonidos[i].Stop();
                return;
            }
        }
        Debug.LogWarning("No hay ese sonido en la lista");
    }

    public void PausarSonido(string nombre)
    {
        for (int i = 0; i < sonidos.Length; i++)
        {
            if (sonidos[i].nombre == nombre)
            {
                sonidos[i].Pause();
                return;
            }
        }
        Debug.LogWarning("No hay ese sonido en la lista");
    }
}
