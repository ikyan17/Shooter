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
        if (collider.gameObject.CompareTag("Trampa"))
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
        if (value.isPressed && Time.time >= nextShotTime)
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
}
