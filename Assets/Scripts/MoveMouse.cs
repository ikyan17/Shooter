using UnityEngine;
using UnityEngine.InputSystem;


public class MoveMouse : MonoBehaviour

{
    [SerializeField] private float sensibilidadMouse;
    private Vector2 mira;

    void Start()
    {

    }

    void Update()
    {
        Move();
    }

    public void OnLook(InputValue value)
    {
        mira = value.Get<Vector2>();
    }

    public void Move()
    {
        float rotacionY = mira.x * sensibilidadMouse * Time.deltaTime;
        transform.Rotate(0, rotacionY, 0);
    }
}