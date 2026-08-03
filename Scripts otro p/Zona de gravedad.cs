using UnityEngine;

public class ZonadeGravedad : MonoBehaviour
{
    public float gravedad = -9;
    private Vector3 vel;

    void Start()
    {

    }

    void Update()
    {
        SlowGravity();
    }

    void SlowGravity()
    {
        vel.y += gravedad * Time.deltaTime;
        transform.position += vel * Time.deltaTime;

        if (transform.position.y <= 1)
        {
            transform.position = new Vector3
                (transform.position.x, -1, transform.position.z);

            vel.y = 6;
        }

        if (transform.position.y <= 2 )
        {
            transform.position = new Vector3
                (transform.position.x, 2, transform.position.z);

            vel.y = 6;
        }
    }
}
// cuando el objeto entre en una zona la gravedad lo afecte