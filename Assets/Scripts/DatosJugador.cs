using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DatosJugador
{
    public int nivel;
    public int puntos;

    public DatosJugador( MoverCientifico cientifico)
    {
        nivel = cientifico.nivelActual;
        puntos = cientifico.puntajeActual;
    }
}
