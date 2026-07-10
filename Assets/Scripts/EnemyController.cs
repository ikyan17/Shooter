using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float detectionRadius = 12.0f;

    [SerializeField] private float vidaMaxima = 30f;
    private float vidaActual;

    // =======================================================
    // ✨ NUEVA VARIABLE: Modificable desde el Inspector
    // =======================================================
    [SerializeField] private float dañoAtaque = 10f;

    private Rigidbody rb;
    private bool muerto = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        vidaActual = vidaMaxima;

        // Busca al jugador automáticamente al nacer
        GameObject jugadorObj = GameObject.FindGameObjectWithTag("Player");
        if (jugadorObj != null)
        {
            player = jugadorObj.transform;
        }
    }

    void FixedUpdate()
    {
        if (muerto) return;

        // 🛡️ SEGURIDAD: Si aún no encuentra al jugador, salimos de la función 
        // para evitar que el script se rompa y se vuelva inmortal.
        if (player == null) return;

        float distancia = Vector3.Distance(transform.position, player.position);

        if (distancia < detectionRadius)
        {
            Vector3 direccion = (player.position - transform.position);
            direccion.y = 0;
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

    void OnCollisionEnter(Collision collision)
    {
        if (muerto) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            Player jugador = collision.gameObject.GetComponent<Player>();
            if (jugador != null)
            {
                // =======================================================
                // MODIFICADO: Ahora usa la variable en lugar del 10f fijo
                // =======================================================
                jugador.TakeDamage(dañoAtaque);
            }
        }
    }

    public void TomarDaño(float cantidad)
    {
        if (muerto) return;

        vidaActual -= cantidad;
        Debug.Log(gameObject.name + " (Melee) dañado. Vida restante: " + vidaActual);

        if (vidaActual <= 0)
        {
            Muerte();
        }
    }

    void Muerte()
    {
        muerto = true;

        // 💥 NUEVO: Le avisa al Player que este enemigo Melee murió
        if (Player.instancia != null)
        {
            Player.instancia.contador++;
            Player.instancia.ActualizarTextoContador();
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
        }

        Destroy(gameObject);
    }
}