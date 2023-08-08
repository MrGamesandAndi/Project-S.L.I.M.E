using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorController : MonoBehaviour
{   
    private Animator anim;
    public GameObject plataformaMovil;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Prender()
    {      
       anim.SetBool("estaActivado", true);
        plataformaMovil.GetComponent<PlataformaMovil>().puedeMoverse = true;
    }
}
