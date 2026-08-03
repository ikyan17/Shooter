
using UnityEngine;


public class NoTocar : MonoBehaviour



{
    public float fuerzaRepulsion;
    public float fuerzaAtraccion;

    public Transform obj;

    public float vel;
    public Vector3 eje = Vector3.up;
    public Transform centro;

    void Start()
    {

    }


    void Update()
    {
        Orbita();
        
    }

    void Orbita()
    {
        
        transform.RotateAround(centro.position, eje, vel * Time.deltaTime);

        
        transform.position = Vector3.MoveTowards(
            transform.position,
            centro.position,
            vel * Time.deltaTime
        );
    }

    


    void OnTriggerStay(Collider obj)
    {
        Vector3 direccion = (transform.position + obj.transform.position).normalized;
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.AddForce(direccion * fuerzaRepulsion);
    }


}

