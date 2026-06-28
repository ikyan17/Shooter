using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Player : Character
{
    
    public static Player instancia;

    [SerializeField] private TextMeshProUGUI vidasText;

    
    [SerializeField] private TextMeshProUGUI textoContador;
    [HideInInspector] public int contador = 0;

    void Awake()
    {
      
        instancia = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

      
        m_vidaActual = m_vida;

        ActualizarTexto();
        ActualizarTextoContador(); 

        Debug.Log(m_vidaActual);
    }

    void FixedUpdate()
    {
        Move();
        Die();
    }

   
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage); 
        ActualizarTexto();       
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
            
        }
    }

    void ActualizarTexto()
    {
        if (vidasText != null)
        {
            vidasText.text = "Vidas: " + m_vidaActual;
        }
    }

    
    public void ActualizarTextoContador()
    {
        if (textoContador != null)
        {
            textoContador.text = "Trampas: " + contador;
        }
    }
}