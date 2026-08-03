using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float detectionRadius = 12.0f;
    [SerializeField] private float vidaMaxima = 30f;
    private float vidaActual;
    [SerializeField] private float dañoAtaque = 10f;

    [Header("Drop")]
    [SerializeField] private GameObject prefabMoneda; // Arrastra aquí el prefab de tu moneda desde el proyecto

    private Transform playerObjetivo; // El jugador al que va a perseguir
    private Rigidbody rb;
    private bool muerto = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        vidaActual = vidaMaxima;
    }

    void FixedUpdate()
    {
        if (muerto) return;

        // 1. Encontrar cuál es el jugador más cercano en este frame
        BuscarJugadorMasCercano();

        // 2. Si no hay jugadores o ninguno está cerca, detener el movimiento horizontal
        if (playerObjetivo == null)
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            return;
        }

        // 3. Evaluar distancia hacia el objetivo encontrado
        float distancia = Vector3.Distance(transform.position, playerObjetivo.position);

        if (distancia < detectionRadius)
        {
            Vector3 direccion = (playerObjetivo.position - transform.position);
            direccion.y = 0; // Mantener la velocidad en el plano horizontal
            direccion.Normalize();

            rb.linearVelocity = new Vector3(direccion.x * speed, rb.linearVelocity.y, direccion.z * speed);

            if (direccion != Vector3.zero)
            {
                transform.forward = direccion;
            }
        }
        else
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
        }
    }

    void BuscarJugadorMasCercano()
    {
        // Encuentra a TODOS los jugadores activos en la escena
        GameObject[] jugadores = GameObject.FindGameObjectsWithTag("Player");

        float distanciaMasCorta = Mathf.Infinity;
        Transform jugadorCercano = null;

        foreach (GameObject p in jugadores)
        {
            float distancia = Vector3.Distance(transform.position, p.transform.position);
            if (distancia < distanciaMasCorta)
            {
                distanciaMasCorta = distancia;
                jugadorCercano = p.transform;
            }
        }

        playerObjetivo = jugadorCercano;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (muerto) return;

        // Le hace daño al jugador con el que chocó
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent<Player>(out var jugador))
            {
                jugador.TakeDamage(dañoAtaque);
            }
        }
    }

    public void TomarDaño(float cantidad)
    {
        if (muerto) return;

        vidaActual -= cantidad;
        Debug.Log(gameObject.name + " (Melee) dañado. Vida restante: " + vidaActual);

        if (vidaActual <= 0) Muerte();
    }

    void Muerte()
    {
        muerto = true;

        if (Player.instancia != null)
        {
            Player.instancia.contador++;
            Player.instancia.ActualizarTextoContador();
        }

        // --- NUEVO: Instanciar la moneda si el prefab está asignado ---
        if (prefabMoneda != null)
        {
            Instantiate(prefabMoneda, transform.position, Quaternion.identity);
        }
        // -------------------------------------------------------------

        rb.linearVelocity = Vector3.zero;
        Destroy(gameObject);
    }
}