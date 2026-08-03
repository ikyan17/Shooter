using UnityEngine;
using UnityEngine.SceneManagement;

public class PasarNivel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Carga la siguiente escena en el Build Settings
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
