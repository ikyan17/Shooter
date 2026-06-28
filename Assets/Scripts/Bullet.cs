using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Trampa"))
        {
            
            if (Player.instancia != null)
            {
                
                Player.instancia.contador++;

                
                Player.instancia.ActualizarTextoContador();
            }

            Destroy(collider.gameObject); 
            Destroy(gameObject);          
        }
    }
}