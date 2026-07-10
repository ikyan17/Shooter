using System.Collections;
using UnityEngine;

public class enemigoshooter : MonoBehaviour
{
    // 1. VARIABLES PÚBLICAS (Se configuran en Unity)
    public Transform spawnBullet;
    public GameObject BulletPrefab;
    public float visionRange = 12f;
    public float shootDelayTime = 1.5f;
    public float lifeTime = 3f;
    public float vidaMaxima = 30f;

    // 2. VARIABLES PRIVADAS (Uso interno)
    private GameObject player;
    private float vidaActual;
    private bool canShoot = true;
    private bool muerto = false;

    void Start()
    {
        vidaActual = vidaMaxima;
        player = GameObject.FindGameObjectWithTag("Player"); // Busca al jugador al iniciar
    }

    void Update()
    {
        // Si el enemigo murió o no encuentra al jugador, no hace nada
        if (muerto || player == null) return;

        // 🔥 ARREGLO DE ROTACIÓN: Forzamos a que mire al jugador pero SIN inclinarse
        // Usamos la posición del jugador pero le ponemos la misma altura (Y) del enemigo
        Vector3 posicionPlana = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(posicionPlana);

        // Calcular distancia. Si está cerca y puede disparar, dispara
        float distancia = Vector3.Distance(transform.position, player.transform.position);
        if (distancia < visionRange && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;

        // Creamos la bala en el punto de spawn
        GameObject nuevaBala = Instantiate(BulletPrefab, spawnBullet.position, spawnBullet.rotation);

        // 🔥 ARREGLO DE FÍSICAS: Desactivamos el choque entre el enemigo y la bala que acaba de crear
        // Obtenemos el colisionador del enemigo y el de la bala, y los hacemos "fantasmas" entre sí
        Collider colisionadorEnemigo = GetComponent<Collider>();
        Collider colisionadorBala = nuevaBala.GetComponent<Collider>();

        if (colisionadorEnemigo != null && colisionadorBala != null)
        {
            Physics.IgnoreCollision(colisionadorEnemigo, colisionadorBala);
        }

        // Le damos velocidad a la bala hacia adelante
        Rigidbody rbBala = nuevaBala.GetComponent<Rigidbody>();
        if (rbBala != null)
        {
            rbBala.linearVelocity = spawnBullet.forward * 20f;
        }

        // Le avisamos a la bala que NO es del jugador
        Bullet scriptBala = nuevaBala.GetComponent<Bullet>();
        if (scriptBala != null)
        {
            scriptBala.esDelJugador = false;
        }

        // Destruimos la bala después de unos segundos y esperamos para el siguiente disparo
        StartCoroutine(DestroyBala(nuevaBala));
        yield return new WaitForSeconds(shootDelayTime);

        canShoot = true;
    }

    IEnumerator DestroyBala(GameObject Bala)
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(Bala);
    }

    public void TomarDaño(float cantidad)
    {
        if (muerto) return;

        vidaActual -= cantidad;

        // Si se queda sin vida, sumamos el punto y se destruye aquí mismo
        if (vidaActual <= 0)
        {
            muerto = true;

            if (Player.instancia != null)
            {
                Player.instancia.contador++;
                Player.instancia.ActualizarTextoContador();
            }

            Destroy(gameObject);
        }
    }
}