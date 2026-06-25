using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Player : Character
{
    [SerializeField] private TextMeshProUGUI vidasText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 1. Asignamos la vida inicial que viene desde la clase padre (Character)
        m_vidaActual = m_vida;

        // 2. Ahora que m_vidaActual ya tiene su valor (ej. 100), pintamos el texto en la interfaz
        ActualizarTexto();

        Debug.Log(m_vidaActual);
    }

    void FixedUpdate()
    {
        Move();
        Die();
    }

    public void OnMove(InputValue inputValue)
    {
        inputMove = inputValue.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && EstaEnSuelo())
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, fuerzaSalto, rb.linearVelocity.z);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        // CASO 1: Choca con una trampa
        if (collider.gameObject.CompareTag("Trampa"))
        {
            DamagePlayer(10);
            ActualizarTexto(); // CORRECCIÓN: Actualiza el texto en pantalla al recibir daño
        }

        // CASO 2: Choca con un enemigo
        if (collider.gameObject.CompareTag("Enemy"))
        {
            
            Destroy(collider.gameObject);
        }
    }

    // CORRECCIÓN: Ahora el texto mostrará directamente la vida numérica actual (m_vidaActual)
    void ActualizarTexto()
    {
        if (vidasText != null)
        {
            vidasText.text = "Vidas: " + m_vidaActual;
        }
    }


}