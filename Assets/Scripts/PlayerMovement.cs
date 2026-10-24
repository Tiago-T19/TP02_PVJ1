using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed;

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    [SerializeField] private float jumpForce;
    [SerializeField] private bool isStop = false;
    public bool IsStop
    {
        get { return isStop; }
        set { isStop = value; }
    }

    private float moveHorizontal;
    private float moveVertical;
    private Rigidbody rb;

    [Header("Raycast Settings")]
    [SerializeField] private bool isGrounded;

    [SerializeField] private float rayDistance;

    [SerializeField] private LayerMask groundLayer;

    [Header("Invisibility")]
    

    // Variable que indica si el jugador está invisible
    private bool isInvisible = false;
    private Renderer[] renderers;
    private Color[] coloresOriginales;

    // Permite consultar desde otros scripts
    public bool IsInvisible
    {
        get { return isInvisible; }
    }



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        renderers = GetComponentsInChildren<Renderer>();
        coloresOriginales = new Color[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            coloresOriginales[i] = renderers[i].material.color;
        }
    }

    void Update()
    {

        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        CheckGround();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
        }

    }

    private void FixedUpdate()
    {
        Vector3 movement = (transform.forward * moveVertical + transform.right * moveHorizontal).normalized;
        rb.linearVelocity = new Vector3(movement.x * speed, rb.linearVelocity.y, movement.z * speed);
    }
    private void CheckGround()
    {
        Vector3 origin = transform.position;
        isGrounded = Physics.Raycast(origin, Vector3.down, rayDistance, groundLayer);

        Color ColorRay = isGrounded ? Color.green : Color.red;

        Debug.DrawRay(origin, Vector3.down * rayDistance, ColorRay);
    }

    public void RotateCharacter(float rotateY)
    {
        transform.rotation = Quaternion.Euler(0f, rotateY, 0f);
    }

    //---------------------------------------------------
    // POWER UP DE INVISIBILIDAD
    //---------------------------------------------------

    public void ActivarInvisibilidad(float duracion)
    {
        // Reinicia la corrutina si el jugador toma otro powerup
        StopCoroutine("InvisibilityRoutine");
        StartCoroutine(InvisibilityRoutine(duracion));
    }

    private System.Collections.IEnumerator InvisibilityRoutine(float duracion)
    {
        isInvisible = true;

        Debug.Log("Invisibilidad activada");

        // Hace al jugador blanco simulando ser invisible
        foreach (Renderer rend in renderers)
        {
            if (rend.material.HasProperty("_Color"))
            {
                rend.material.color = Color.blue;
            }
        }

        // Espera la duración indicada
        yield return new WaitForSeconds(duracion);

        isInvisible = false;

        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i].material.HasProperty("_Color"))
            {
                renderers[i].material.color = coloresOriginales[i];
            }
        }
        Debug.Log("Invisibilidad terminada");
    }


}

