using UnityEngine;
using UnityEngine.InputSystem;

public class Shot : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject bullet;
    public float shotForce = 1500f;
    public float shotRate = 0.5f;

    private float nextShotTime;

    public void OnAttack(InputValue value)
    {
        if (value.isPressed && Time.time >= nextShotTime)
        {
            nextShotTime = Time.time + shotRate;

         
            GameObject newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotForce);
        }
    }


   

}