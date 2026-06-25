using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Se ejecuta cuando la bala atraviesa a un enemigo (Trigger)
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            Destroy(collider.gameObject); // Destruye al enemigo
            Destroy(gameObject);          // Destruye la bala al impactar
        }
    }
}