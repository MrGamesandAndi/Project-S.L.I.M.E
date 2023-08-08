using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaCrafteo : MonoBehaviour
{
    public List<CreacionDePociones> listaPociones;
    Inventario inventario;
    private MoverCientifico cientifico;

    public Transform puntoDisparo;
    public List<GameObject> efectoATirar;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        inventario = GetComponent<Inventario>();
        anim = gameObject.GetComponent<Animator>();
        cientifico = FindObjectOfType<MoverCientifico>();
    }

    // Update is called once per frame
    void Update()
    {
        if (KeyBindingManager.GetKeyDown(KeyAction.mezclar) && transform.parent==null)
        {
            string ingrediente1 = inventario.slots[0].name;
            string ingrediente2 = inventario.slots[1].name;           
            MezclarPocion(ingrediente1, ingrediente2);         
        }
    }

    public void MezclarPocion(string ingre1, string ingre2)
    {
        for (int i = 0; i < listaPociones.Count; i++)
        {
            if (ingre1 == listaPociones[i].ingrediente1 && ingre2 == listaPociones[i].ingrediente2 ||
                ingre2 == listaPociones[i].ingrediente1 && ingre1 == listaPociones[i].ingrediente2)
            {               
                StartCoroutine("AnimacionMezcla",i);
                LimpiarInventario();
            }
        }

    }
    IEnumerator AnimacionMezcla(int i)
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);
        anim.SetBool("estaMezclando", true);
        QuitarMov(false);
        Vector3 posicion = new Vector3(puntoDisparo.position.x,puntoDisparo.position.y+listaPociones[i].posicionSpawn,puntoDisparo.position.z);
        yield return new WaitForSeconds(1.5f);
        Instantiate(efectoATirar[i], posicion , puntoDisparo.rotation);
        anim.SetBool("estaMezclando", false);
        QuitarMov(true);
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }

    public void LimpiarInventario()
    {
        int i = 0;
        foreach (var item in inventario.slots)
        {         
            inventario.slots[i].name = "Slot(" + i + ")";
            Debug.Log("Se borro" + inventario.slots[i]);
            Destroy(inventario.slots[i].transform.GetChild(0).gameObject);
            i++;          
        }
    }

    public void QuitarMov(bool estado)
    {
        MoverCientifico.puedeMoverse = estado;
    }
}
