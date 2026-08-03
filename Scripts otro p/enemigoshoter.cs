using System.Collections;
using UnityEngine;

public class enemigoshooter : MonoBehaviour
{
    public Transform spawnBullet;
    public GameObject BulletPrefab;
    public float visionRange = 12f;
    public float shootDelayTime = 1.5f;
    public float lifeTime = 3f;
    public float vidaMaxima = 30f;

    private Transform playerObjetivo; // Ahora guardamos el Transform del jugador objetivo
    private float vidaActual;
    private bool canShoot = true;
    private bool muerto = false;

    void Start()
    {
        vidaActual = vidaMaxima;
    }

    void Update()
    {
        if (muerto) return;

        // 1. Buscar cuál es el jugador más cercano en cada frame
        BuscarJugadorMasCercano();

        if (playerObjetivo == null) return;

        // 2. Apuntar al objetivo
        Vector3 posicionPlana = new Vector3(playerObjetivo.position.x, transform.position.y, playerObjetivo.position.z);
        transform.LookAt(posicionPlana);

        // 3. Evaluar distancia y disparar
        float distancia = Vector3.Distance(transform.position, playerObjetivo.position);
        if (distancia < visionRange && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    void BuscarJugadorMasCercano()
    {
        // Encuentra a TODOS los jugadores en la escena (Jugador 1, Jugador 2, etc.)
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

    IEnumerator Shoot()
    {
        canShoot = false;

        GameObject nuevaBala = Instantiate(BulletPrefab, spawnBullet.position, spawnBullet.rotation);

        if (nuevaBala.TryGetComponent<Rigidbody>(out var rbBala))
        {
            rbBala.linearVelocity = spawnBullet.forward * 20f;
        }

        if (nuevaBala.TryGetComponent<Bullet>(out var scriptBala))
        {
            scriptBala.esDelJugador = false;
        }

        Destroy(nuevaBala, lifeTime);

        yield return new WaitForSeconds(shootDelayTime);
        canShoot = true;
    }

    public void TomarDaño(float cantidad)
    {
        if (muerto) return;

        vidaActual -= cantidad;

        if (vidaActual <= 0)
        {
            muerto = true;

            // Ojo: Si usas un singleton (Player.instancia) para el contador, 
            // asegúrate de adaptarlo si cada jugador tiene su propia puntuación.
            if (Player.instancia != null)
            {
                Player.instancia.contador++;
                Player.instancia.ActualizarTextoContador();
            }

            Destroy(gameObject);
        }
    }
}