using System.Collections;
using UnityEngine;

public class enemigoshooter : MonoBehaviour
{
    [SerializeField] private Transform spawnBullet;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float visionRange;
    [SerializeField] private float shootDelayTime;
    [SerializeField] private float movementTime;
    [SerializeField] private float movementRange;

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

        GameObject newBullet = Instantiate(bulletPrefab, spawnBullet.position, spawnBullet.rotation);
        
        Rigidbody rb = newBullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = spawnBullet.forward * 20f;
        }

        yield return new WaitForSeconds(shootDelayTime);
        
        canShoot = true;
    }
}