using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemigoPrefab; // Arrastra aquí tu prefab de enemigo
    public float tiempoInicial = 2f; // Tiempo antes del primer spawn
    public float intervaloSpawn = 3f; // Tiempo entre cada spawn

    void Start()
    {
        // Llama a la función GenerarEnemigo cada 'intervaloSpawn' segundos
        InvokeRepeating("GenerarEnemigo", tiempoInicial, intervaloSpawn);
    }

    void GenerarEnemigo()
    {
        // Posición aleatoria dentro de un rango en el eje X e Y
        Vector3 posicionSpawn = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);

        // Crea el enemigo en la posición indicada
        Instantiate(enemigoPrefab, posicionSpawn, Quaternion.identity);
    }
}
