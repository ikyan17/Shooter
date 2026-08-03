using UnityEngine;

public class Vortice : MonoBehaviour
{
    public float vel = 5f;
    public Vector3 eje = Vector3.up;
    public Transform centro;

    void Update()
    {
        if (centro != null)
        {
            Orbita();
        }
    }

    void Orbita()
    {
        
        transform.RotateAround(centro.position, eje, vel * Time.deltaTime);

        
        transform.position = Vector3.MoveTowards(
            transform.position,
            centro.position,
            vel * Time.deltaTime * 0.1f 
        );
    }
}
