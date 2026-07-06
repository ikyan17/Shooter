using System.Collections;
using UnityEngine;

public class enemigoshooter : MonoBehaviour
{
    [SerializeField] private Transform spawnBala;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject BalaPrefab;

    [SerializeField] private float visionRange;
    [SerializeField] private float shootDelayTime;
    [SerializeField] private float movementTime;
    [SerializeField] private float movementRange;
    [SerializeField] public float lifeTime = 3f;

    [SerializeField] private bool canShoot = true;

    void Start()
    {


    }

    void Update()
    {
        DetectedPlayer();
    }

    void DetectedPlayer()
    {
        if (player != null)
        {
            transform.LookAt(player.transform);

            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance < visionRange)
            {
                if (canShoot)
                {
                    StartCoroutine(Shoot());
                }
            }
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;

        GameObject newBullet = Instantiate(BalaPrefab, spawnBala.position, spawnBala.rotation);
        Rigidbody rb = newBullet.GetComponent<Rigidbody>();

        StartCoroutine(DestroyBala(newBullet));


        if (rb != null)
        {
            rb.linearVelocity = spawnBala.forward * 20f;
        }

        yield return new WaitForSeconds(shootDelayTime);

        canShoot = true;
    }




    private IEnumerator DestroyBala(GameObject Bala)
    {
        yield return new WaitForSeconds(lifeTime);


        Destroy(Bala);

    }
}
