using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Player : Character
{
    [SerializeField] private TextMeshProUGUI vidasText;
    [SerializeField] private TextMeshProUGUI EnemigosText;
    private int Enemigos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        m_vidaActual = m_vida;
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
        
        if (collider.gameObject.CompareTag("Trampa"))
        {
            DamagePlayer(10);
            ActualizarTexto();
        }

       
        if (collider.gameObject.CompareTag("Enemy"))
        {
            Enemigos++; 
            ActualizarTexto();
        }
    }
    void ActualizarTexto()
    {
        if (vidasText != null)
        {
            vidasText.text = "Vidas: " + m_vidaActual;
        }

        if (EnemigosText != null)
        {
            EnemigosText.text = "Enemigos: " + Enemigos;
        }
    }
}