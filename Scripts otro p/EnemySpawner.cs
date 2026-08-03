using UnityEngine;
using UnityEngine.InputSystem;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemigoPrefab;
    public float tiempoInicial = 2f;
    public float intervaloSpawn = 3f;

    void Start()
    {
        InvokeRepeating("GenerarEnemigo", tiempoInicial, intervaloSpawn);
    }

    void GenerarEnemigo()
    {
        Vector3 posicionSpawn = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);

        // Instantiate stores a reference to the newly created game object in the scene
        GameObject nuevoEnemigo = Instantiate(enemigoPrefab, posicionSpawn, Quaternion.identity);

        // Apply a random color to the active instance
        CambiarColor(nuevoEnemigo, Random.ColorHSV());
    }

    // Renamed to avoid confusion and properly change the instance's color
    void CambiarColor(GameObject obj, Color nuevoColor)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = nuevoColor;
        }
    }
}