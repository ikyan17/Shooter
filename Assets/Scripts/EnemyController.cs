using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float detectionRadius = 12.0f;
    [SerializeField] private int vida = 3;

    private Rigidbody rb;
    private bool muerto = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (player == null)
        {
            GameObject jugadorObj = GameObject.FindGameObjectWithTag("Player");
            if (jugadorObj != null) player = jugadorObj.transform;
        }
    }

    void FixedUpdate()
    {
        if (muerto || player == null || rb == null) return;

        
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
            jugador?.DamagePlayer(10f);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (muerto) return;

        if (collision.gameObject.CompareTag("Bala"))
        {
            vida--;
            if (vida <= 0) Muerte();
        }
    }

    void Muerte()
    {
        muerto = true;
        if (rb != null) rb.linearVelocity = Vector3.zero;

        foreach (Collider col in GetComponents<Collider>())
        {
            col.enabled = false;
        }

        Destroy(gameObject, 0.5f);
    }
}