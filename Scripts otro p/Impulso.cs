using UnityEngine;

public class Impulso : MonoBehaviour
{
    public float gravedad = -9f;
    public float impulsoInicial = -15f;
    private float velY = 0f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
               
                rb.AddForce(new Vector3(0, impulsoInicial, 0), ForceMode.Impulse);

                velY = rb.linearVelocity.y;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                
                velY += gravedad * Time.deltaTime;

               
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, velY, rb.linearVelocity.z);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                velY = 0f;
            }
        }
    }
}