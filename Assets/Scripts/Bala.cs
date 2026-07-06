using UnityEngine;

public class Bala : MonoBehaviour
{
    
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {

            Destroy(collider.gameObject);
            Destroy(gameObject);
        }

    }
}