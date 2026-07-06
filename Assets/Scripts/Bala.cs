using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 3f;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {

            Destroy(collider.gameObject);
            Destroy(gameObject);
        }

    }
}