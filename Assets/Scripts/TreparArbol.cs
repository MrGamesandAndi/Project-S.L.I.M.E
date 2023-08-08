using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreparArbol : MonoBehaviour
{
    public float velocidad=10f;
    public float distancia=5f;
    public LayerMask arbol;
    private bool estaTrepando;
    private float inputVertical;
    private Rigidbody2D rb;
    private Animator doctorAnimator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        doctorAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        RaycastHit2D info = Physics2D.Raycast(transform.position, Vector2.up, distancia, arbol);
        if (info.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                doctorAnimator.SetBool("subiendo", true);
                estaTrepando = true;
            }
        }
        else
        {
            doctorAnimator.SetBool("subiendo", false);
            estaTrepando = false;
        }

        if (estaTrepando)
        {
            inputVertical = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, inputVertical * velocidad);
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 1;
        }
    }
}
