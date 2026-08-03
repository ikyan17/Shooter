using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public bool esDelJugador = true;

    void OnTriggerEnter(Collider collider)
    {
        if (esDelJugador) ManejarImpactoJugador(collider);
        else ManejarImpactoEnemigo(collider);
    }

    private void ManejarImpactoJugador(Collider collider)
    {
        if (collider.CompareTag("Torreta"))
        {
            enemigoshooter torreta = collider.GetComponent<enemigoshooter>();
            if (torreta != null) torreta.TomarDaño(10f);

            Destroy(gameObject);
        }
        else if (collider.CompareTag("Enemy"))
        {
            // Intentamos dañar al enemigo estándar
            EnemyController enemigo = collider.GetComponent<EnemyController>();
            if (enemigo != null)
            {
                enemigo.TomarDaño(10f);
                Destroy(gameObject);
                return;
            }

            // Intentamos dañar al enemigo volador recién creado
            EnemigoVolador enemigoVolador = collider.GetComponent<EnemigoVolador>();
            if (enemigoVolador != null)
            {
                enemigoVolador.TomarDaño(10f);
                Destroy(gameObject);
                return;
            }
        }
    }

    private void ManejarImpactoEnemigo(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Player jugador = collider.GetComponent<Player>();
            if (jugador != null) jugador.TakeDamage(10f);

            Destroy(gameObject);
        }
    }
}