using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Por defecto es true, pero el enemigo la cambiará a false al disparar
    [HideInInspector] public bool esDelJugador = true;

    void OnTriggerEnter(Collider collider)
    {
        // --- LÓGICA SI LA BALA LA DISPARÓ EL JUGADOR ---
        if (esDelJugador)
        {
            if (collider.gameObject.CompareTag("Trampa"))
            {
                Player.instancia.contador++;
                Player.instancia.ActualizarTextoContador();

                Destroy(collider.gameObject);
                Destroy(gameObject);
            }
            // Puedes agregar esto si quieres que también dañe enemigos:
            else if (collider.gameObject.CompareTag("Enemigo"))
            {
                Destroy(collider.gameObject); // O la lógica de daño que uses
                Destroy(gameObject);
            }
        }
        // --- LÓGICA SI LA BALA LA DISPARÓ EL ENEMIGO ---
        else
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                // Aquí puedes restar vida al jugador, por ejemplo:
                // collider.gameObject.GetComponent<Player>().RestarVida(1);

                Destroy(gameObject); // Destruye la bala al tocar al jugador
            }

            // Si quieres que las balas enemigas también rompan trampas sin dar puntos:
            else if (collider.gameObject.CompareTag("Trampa"))
            {
                Destroy(collider.gameObject);
                Destroy(gameObject);
            }
        }
    }
}