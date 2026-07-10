using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public bool esDelJugador = true;

    
    void OnTriggerEnter(Collider collider)
    {
        if (esDelJugador)
        {
            ManejarImpactoJugador(collider);
        }
        else
        {
            ManejarImpactoEnemigo(collider);
        }
    }

    
    private void ManejarImpactoJugador(Collider collider)
    {
        
        if (collider.CompareTag("Trampa"))
        {
            enemigoshooter trampa = collider.GetComponentInParent<enemigoshooter>();
            if (trampa != null) trampa.TomarDaño(10f);

            Destroy(gameObject)
        }
        
        else if (collider.CompareTag("Enemy"))
        {
            EnemyController enemigo = collider.GetComponentInParent<EnemyController>();
            if (enemigo != null) enemigo.TomarDaño(10f);

            Destroy(gameObject); 
        }
    }

    
    private void ManejarImpactoEnemigo(Collider collider)
    {
        // ¿Chocó con el Jugador?
        if (collider.CompareTag("Player"))
        {
            Player jugador = collider.GetComponent<Player>();
            if (jugador != null) jugador.TakeDamage(10f);

            Destroy(gameObject); // Rompe la bala enemiga
        }
    }
}