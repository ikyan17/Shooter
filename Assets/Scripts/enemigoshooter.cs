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

    private GameObject player;
    private float vidaActual;
    private bool canShoot = true;
    private bool muerto = false;

    void Start()
    {
        vidaActual = vidaMaxima;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (muerto || player == null) return;

        Vector3 posicionPlana = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(posicionPlana);

        float distancia = Vector3.Distance(transform.position, player.transform.position);
        if (distancia < visionRange && canShoot)
        {
            StartCoroutine(Shoot());
        }
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

            if (Player.instancia != null)
            {
                Player.instancia.contador++;
                Player.instancia.ActualizarTextoContador();
            }

            Destroy(gameObject);
        }
    }
}
