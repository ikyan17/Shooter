using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravedad = 9;
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
        //if (transform.position.y <= 0)
        //{
        //    transform.position = new Vector3
        //        (transform.position.x, 0, transform.position.z);

        //    vel.y = 0;
        //}

        if (transform.position.y >= 6)
        {
            transform.position = new Vector3
                (transform.position.x, 6, transform.position.z);

            vel.y = 0;
        }
    }
}
