using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Shot : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject bullet;
    public float shotForce = 1500f;
    public float shotRate = 0.5f;
    public float lifeTime = 3f; 
    private float nextShotTime;

    public void OnAttack(InputValue value)
    {
        if (value.isPressed && Time.time >= nextShotTime)
        {
            nextShotTime = Time.time + shotRate;

            GameObject newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotForce);

            
            StartCoroutine(DestroyBullet(newBullet));
        }
    }

    
    private IEnumerator DestroyBullet(GameObject Bullet)
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(Bullet);
    }
}