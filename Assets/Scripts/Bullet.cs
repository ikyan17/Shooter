using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            Destroy(collider.gameObject); 
            Destroy(gameObject);          
        }
    }
}