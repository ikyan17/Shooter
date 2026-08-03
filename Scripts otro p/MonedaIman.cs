using UnityEngine;

public class MonedaIman : MonoBehaviour
{
    public float fuerzaAtraccion;
    public Transform obj;

    void Start()
    {
        if (obj == null)
        {
            obj = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (obj != null)
        {
            Atraccion();
        }
    }

    void Atraccion()
    {
        transform.position = Vector3.MoveTowards(transform.position, obj.position, fuerzaAtraccion * Time.deltaTime);
    }
}