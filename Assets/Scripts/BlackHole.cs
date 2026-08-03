using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float fuerzaAtraccion;
    
    public Transform obj;

    void Start()
    {
        
    }

    
    void Update()
    {
        Atraccion();
    }

    void Atraccion()
    {
        Vector3 direccion = (transform.position - obj.position).normalized;
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.AddForce (direccion * fuerzaAtraccion);
    }
}
