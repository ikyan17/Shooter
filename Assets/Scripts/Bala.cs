using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 3f; // Para que no exista para siempre

    void Start()
    {
        // Destruir la bala después de unos segundos para optimizar
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Mueve la bala hacia adelante constantemente
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}