using System.Collections;
using UnityEngine;

public class enemigoshooter : MonoBehaviour
{
    [SerializeField] private Transform spawnBullet;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject BulletPrefab;

    [SerializeField] private float visionRange;
    [SerializeField] private float shootDelayTime;
    [SerializeField] private float movementTime;
    [SerializeField] private float movementRange;
    [SerializeField] public float lifeTime = 3f;

    [SerializeField] private bool canShoot = true;
    [SerializeField] private bool esDelJugador = true;
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

        GameObject newBullet = Instantiate(BulletPrefab, spawnBullet.position, spawnBullet.rotation);
        Rigidbody rb = newBullet.GetComponent<Rigidbody>();

        Bullet scriptBala = newBullet.GetComponent<Bullet>();
        if (scriptBala != null)
        {
            scriptBala.esDelJugador = false;
        }

        StartCoroutine(DestroyBala(newBullet));

        if (rb != null)
        {
            rb.linearVelocity = spawnBullet.forward * 20f;
        }

        yield return new WaitForSeconds(shootDelayTime);
        canShoot = true;
    }





    private IEnumerator DestroyBala(GameObject Bullet)
    {
        yield return new WaitForSeconds(lifeTime);
               
        
            Destroy(Bullet);
        
    }
    

}
