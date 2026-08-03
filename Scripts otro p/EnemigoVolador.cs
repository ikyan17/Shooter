using System.Collections;
using UnityEngine;

public class EnemigoVolador : MonoBehaviour
{
    [Header("Combat & Vision")]
    public Transform spawnBullet;
    public GameObject BulletPrefab;
    public float visionRange = 12f;
    public float shootDelayTime = 1.5f;
    public float lifeTime = 3f;
    public float vidaMaxima = 30f;

    [Header("Flight & Random Movement")]
    public float moveSpeed = 4f;
    public float alturaVuelo = 3f;      // Altura constante sobre la posición inicial o el suelo
    public float cambioDestinoTiempo = 3f; // Tiempo para elegir un nuevo punto aleatorio
    public float radioMovimiento = 5f;     // Radio máximo alrededor de su posición inicial para moverse

    [Header("Drops")]
    public GameObject monedaPrefab;     // Prefab de la moneda que soltará al morir

    private Transform playerObjetivo;
    private float vidaActual;
    private bool canShoot = true;
    private bool muerto = false;

    // Variables para el movimiento aleatorio
    private Vector3 posicionInicial;
    private Vector3 objetivoAleatorio;
    private float temporizadorCambioDestino;

    void Start()
    {
        vidaActual = vidaMaxima;
        posicionInicial = transform.position;
        ObtenerNuevoDestinoAleatorio();
    }

    void Update()
    {
        if (muerto) return;

        // 1. Buscar cuál es el jugador más cercano en cada frame (solo para disparar/apuntar si está en rango)
        BuscarJugadorMasCercano();

        // 2. Movimiento aleatorio constante
        MoverDeManeraAleatoria();

        if (playerObjetivo == null) return;

        // 3. Apuntar al jugador (mantiene la rotación hacia él aunque se mueva aleatoriamente)
        Vector3 posicionPlana = new Vector3(playerObjetivo.position.x, transform.position.y, playerObjetivo.position.z);
        transform.LookAt(posicionPlana);

        // 4. Evaluar rango de visión y disparar
        float distanciaAlJugador = Vector3.Distance(transform.position, playerObjetivo.position);
        if (distanciaAlJugador < visionRange && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    void BuscarJugadorMasCercano()
    {
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

    void MoverDeManeraAleatoria()
    {
        // Reducir temporizador para cambiar de destino
        temporizadorCambioDestino -= Time.deltaTime;
        if (temporizadorCambioDestino <= 0f || Vector3.Distance(transform.position, objetivoAleatorio) < 0.5f)
        {
            ObtenerNuevoDestinoAleatorio();
        }

        // Moverse hacia el punto aleatorio calculado
        transform.position = Vector3.MoveTowards(transform.position, objetivoAleatorio, moveSpeed * Time.deltaTime);
    }

    void ObtenerNuevoDestinoAleatorio()
    {
        // Generar una posición aleatoria dentro de una esfera alrededor de la posición inicial
        Vector3 desplazamientoAleatorio = Random.insideUnitSphere * radioMovimiento;
        objetivoAleatorio = posicionInicial + desplazamientoAleatorio;

        // Asegurar que respete la altura de vuelo deseada
        objetivoAleatorio.y = posicionInicial.y + alturaVuelo;

        // Reiniciar el temporizador
        temporizadorCambioDestino = cambioDestinoTiempo;
    }

    IEnumerator Shoot()
    {
        canShoot = false;

        // Instanciamos la bala
        GameObject nuevaBala = Instantiate(BulletPrefab, spawnBullet.position, spawnBullet.rotation);

        // Calculamos la dirección exacta desde el punto de disparo hasta el jugador objetivo
        Vector3 direccionDisparo = (playerObjetivo.position - spawnBullet.position).normalized;

        if (nuevaBala.TryGetComponent<Rigidbody>(out var rbBala))
        {
            // Usamos la dirección hacia el jugador multiplicada por la velocidad
            rbBala.linearVelocity = direccionDisparo * 20f;
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

            // Soltar la moneda si el prefab está asignado
            if (monedaPrefab != null)
            {
                Instantiate(monedaPrefab, transform.position, Quaternion.identity);
            }

            if (Player.instancia != null)
            {
                Player.instancia.contador++;
                Player.instancia.ActualizarTextoContador();
            }

            Destroy(gameObject);
        }
    }
}