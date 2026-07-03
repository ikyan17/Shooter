using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class enemigoshooter : MonoBehaviour


{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject Bullet;

    [SerializeField] private Transform pointBullet;

    [SerializeField] private float distanciaDeteccion;
    [SerializeField] private float timeBullet;

    [SerializeField] bool Disparo;

    void Start()
    {

    }


    void Update()
    {
        DetectionDistance();
    }

    void DetectionDistance()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= distanciaDeteccion && Disparo)
        {
            StartCoroutine(TimeBullet());
        }
    }

    IEnumerator TimeBullet()
    {
        Disparo = false;
        yield return new WaitForSeconds(timeBullet);

        Instantiate(Bullet, pointBullet.position, pointBullet.rotation);

        Disparo = true;
    }
}