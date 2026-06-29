using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Trampa"))
        {
                                                 
                Player.instancia.contador++;       
                Player.instancia.ActualizarTextoContador();
            

            Destroy(collider.gameObject); 
            Destroy(gameObject);          
        }
    }
}