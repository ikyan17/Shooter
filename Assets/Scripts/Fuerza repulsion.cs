using UnityEngine;

public class Fuerzarepulsion : MonoBehaviour
{
    public float fuerzaRepulsion;

    public Transform obj;

    void Start()
    {

    }


    void Update()
    {
        Repulsion();
    }

    void Repulsion()
    {
        Vector3 direccion = (transform.position - obj.position).normalized;
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.AddForce(direccion * fuerzaRepulsion);
    }
}

