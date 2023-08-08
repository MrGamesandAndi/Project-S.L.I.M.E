using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Pocion_",menuName ="Pociones")]
public class CreacionDePociones : ScriptableObject
{
    public string ingrediente1;
    public string ingrediente2;
    public int posicionSpawn=0;
}
