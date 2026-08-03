using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class Player : Character
{
    public static Player instancia;

    [SerializeField] private TextMeshProUGUI vidasText;
    [SerializeField] private TextMeshProUGUI textoContador;
    [SerializeField] private TextMeshProUGUI monedasText;

    [HideInInspector] public int contador;
    private int monedasColectadas = 0;

    [Header("Disparo")]
    public Transform spawnPoint;
    public GameObject bullet;
    public float shotForce = 1500f;
    public float shotRate = 0.5f;
    public float lifeTime = 3f;
    private float nextShotTime;

    [Header("Modo de Disparo")]
    [SerializeField] private bool esDisparoAutomatico = false;
    private bool estaDisparandoAutomatico = false;

    [SerializeField] private float sensibilidadMouse;
    private Vector2 mira;

    [Header("Configuración del Dash")]
    [SerializeField] private float fuerzaDash = 25f;
    [SerializeField] private float duracionDash = 0.2f;
    [SerializeField] private float cooldownDash = 1f;
    private bool puedeDash = true;
    private bool estaDashing = false;

    public Animator animator;

    // Control de Correr (Modo Interruptor)
    private bool estaCorriendo = false;

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
            Move(estaCorriendo);
        }

        Die();
    }

    void Update()
    {
        // 1. Manejo del disparo automático independiente
        if (esDisparoAutomatico && estaDisparandoAutomatico)
        {
            IntentarDisparar();
        }

        // 2. Actualización constante de las animaciones de movimiento
        ActualizarAnimaciones();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        ActualizarTexto();
    }

    public void OnMove(InputValue inputValue)
    {
        inputMove = inputValue.Get<Vector2>();

        if (inputMove == Vector2.zero)
        {
            estaCorriendo = false;
        }
    }

    public void OnSprint(InputValue value)
    {
        if (value.isPressed)
        {
            estaCorriendo = !estaCorriendo;
            Debug.Log("¿Está corriendo?: " + estaCorriendo);
        }
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && EstaEnSuelo() && !estaDashing)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, fuerzaSalto, rb.linearVelocity.z);
        }
    }

    public void OnDash(InputValue value)
    {
        if (value.isPressed && puedeDash && !estaDashing)
        {
            StartCoroutine(EjecutarDash());
        }
    }

    private IEnumerator EjecutarDash()
    {
        puedeDash = false;
        estaDashing = true;

        Vector3 direccionDash = new Vector3(inputMove.x, 0f, inputMove.y).normalized;

        if (direccionDash == Vector3.zero)
        {
            direccionDash = transform.forward;
        }

        float tiempoInicio = Time.time;

        while (Time.time < tiempoInicio + duracionDash)
        {
            rb.linearVelocity = new Vector3(direccionDash.x * fuerzaDash, rb.linearVelocity.y, direccionDash.z * fuerzaDash);
            yield return new WaitForFixedUpdate();
        }

        estaDashing = false;

        yield return new WaitForSeconds(cooldownDash);
        puedeDash = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Torreta"))
        {
            DamagePlayer(10);
        }

        if (collider.gameObject.CompareTag("Moneda"))
        {
            monedasColectadas++;
            ActualizarTextoMonedas();
            Destroy(collider.gameObject);

            if (monedasColectadas >= 10)
            {
                SceneManager.LoadScene("You win");
            }
        }
    }

    void ActualizarTexto()
    {
        vidasText.text = "Vidas: " + m_vidaActual;
    }

    public void ActualizarTextoContador()
    {
        textoContador.text = "Enemigos: " + contador;
    }

    void ActualizarTextoMonedas()
    {
        if (monedasText != null)
        {
            monedasText.text = "Monedas: " + monedasColectadas;
        }
    }

    public void OnAttack(InputValue value)
    {
        if (esDisparoAutomatico)
        {
            // Si está en modo automático, mantenemos el estado de si se presiona el botón para disparar sin parar
            estaDisparandoAutomatico = value.isPressed;
        }
        else
        {
            // Si es disparo normal, solo dispara una vez al presionar
            if (value.isPressed)
            {
                IntentarDisparar();
            }
        }
    }

    private void IntentarDisparar()
    {
        if (Time.time >= nextShotTime)
        {
            nextShotTime = Time.time + shotRate;

            GameObject newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotForce);

            StartCoroutine(DestroyBullet(newBullet));
        }
    }

    private IEnumerator DestroyBullet(GameObject Bullet)
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(Bullet);
    }

    private void ActualizarAnimaciones()
    {
        if (animator == null) return;

        // Multiplicador para escalar la velocidad si está corriendo (ej. para Blend Trees)
        float multiplicadorCorrer = estaCorriendo ? 2f : 1f;

        float velX = inputMove.x * multiplicadorCorrer;
        float velZ = inputMove.y * multiplicadorCorrer;

        animator.SetFloat("VelX", velX);
        animator.SetFloat("VelZ", velZ);
        animator.SetBool("isSprinting", estaCorriendo);
    }

    public void OnAutoFire(InputValue value)
    {
        if (value.isPressed)
        {
            esDisparoAutomatico = !esDisparoAutomatico;
            Debug.Log("Modo automático activado: " + esDisparoAutomatico);
        }
    }

    internal void SwitchOnCamera()
    {
        throw new NotImplementedException();
    }
}