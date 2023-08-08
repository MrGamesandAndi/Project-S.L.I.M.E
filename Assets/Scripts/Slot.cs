using UnityEngine;

public class Slot : MonoBehaviour
{
    private Inventario inventario;
    public int i;

    private void Start()
    {
        inventario = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventario>();
    }

    private void Update()
    {
        if(transform.childCount<=0)
        {
            inventario.estaLleno[i] = false;
        }
    }

    public void tirarSlime()
    {
        foreach (Transform hijo in transform)
        {
            inventario.slots[i].name = "Slot(" + i + ")";
            Debug.Log("Se borro" + hijo.name);
            Destroy(hijo.gameObject);
        }
    }
}
