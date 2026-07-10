using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : Character
{
    public static Player instancia;

    [SerializeField] private TextMeshProUGUI vidasText;
    [SerializeField] private TextMeshProUGUI textoContador;
    [SerializeField] private TextMeshProUGUI monedasText;

    [HideInInspector] public int contador = 0;
    private int monedasColectadas = 0;

    [Header("Disparo")]
    public Transform spawnPoint;
    public GameObject bullet;
    public float shotForce = 1500f;
    public float shotRate = 0.5f;
    public float lifeTime = 3f;
    private float nextShotTime;

    [SerializeField] private float sensibilidadMouse;
    private Vector2 mira;

    [Header("Configuración del Dash")]
    [SerializeField] private float fuerzaDash = 25f;       
    [SerializeField] private float duracionDash = 0.2f;    
    [SerializeField] private float cooldownDash = 1f;      
    private bool puedeDash = true;
    private bool estaDashing = false;

    void Awake()
    {
        instancia = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        m_vidaActual = m_vida;
        Debug.Log(m_vidaActual);
        ActualizarTexto();
        ActualizarTextoContador();
        ActualizarTextoMonedas();
    }

    void FixedUpdate()
    {
        if (!estaDashing)
        {
            Move();
        }

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
        if (value.isPressed && EstaEnSuelo() && !estaDashing)
        {
            rb.linearVelocity = new Vector3(rb.
