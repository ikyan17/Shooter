using UnityEngine;

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

        
        Instantiate(enemigoPrefab, posicionSpawn, Quaternion.identity);
    }
}
