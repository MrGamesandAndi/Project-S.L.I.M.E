using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SistemaGuardado
{
    public static void Guardar(MoverCientifico cientifico)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/jugador.slime";
        FileStream stream = new FileStream(path, FileMode.Create);

        DatosJugador datos = new DatosJugador(cientifico);

        formatter.Serialize(stream, datos);
        stream.Close();
    }

    public static DatosJugador Cargar()
    {
        string path = Application.persistentDataPath + "/jugador.slime";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            DatosJugador datos = formatter.Deserialize(stream) as DatosJugador;
            stream.Close();
            return datos;
        }
        else
        {
            Debug.LogError("No hay nada en:" + path);
            return null;
        }
    }

    public static void Borrar()
    {
        string path = Application.persistentDataPath + "/jugador.slime";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        else
        {
            Debug.LogError("No hay nada en:" + path);
        }
    }

    public static bool Verificar()
    {
        string path = Application.persistentDataPath + "/jugador.slime";
        if (File.Exists(path))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
