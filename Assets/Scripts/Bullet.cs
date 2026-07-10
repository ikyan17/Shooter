using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public bool esDelJugador = true;

    // El punto de entrada ahora es súper corto y limpio
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

    // ==========================================
    // 🏹 LÓGICA DE LA BALA DEL JUGADOR
    // ==========================================
    private void ManejarImpactoJugador(Collider collider)
    {
        // ¿Chocó con una Trampa/Shooter?
        if (collider.CompareTag("Trampa"))
        {
            enemigoshooter trampa = collider.GetComponentInParent<enemigoshooter>();
            if (trampa != null) trampa.TomarDaño(10f);

            Destroy(gameObject); // Rompe la bala
        }
        // ¿Chocó con un Enemigo Melee?
        else if (collider.CompareTag("Enemy"))
        {
            EnemyController enemigo = collider.GetComponentInParent<EnemyController>();
            if (enemigo != null) enemigo.TomarDaño(10f);

            Destroy(gameObject); // Rompe la bala
        }
    }

    // ==========================================
    // 👿 LÓGICA DE LA BALA DEL ENEMIGO
    // ==========================================
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