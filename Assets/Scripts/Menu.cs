
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Iniciando juego");
        SceneManager.LoadScene(1);
    }
}