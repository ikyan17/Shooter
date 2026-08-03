using UnityEngine;
using UnityEngine.InputSystem;

public class MoveMouseMultiplayer : MonoBehaviour
{
    [SerializeField] private float sensibilidad = 100f;
    [SerializeField] private Transform camara; // La cámara individual de ESTE jugador

    private Vector2 mira;
    private float rotacionX = 0f;

    void Start()
    {
        // En multiplayer local solo bloqueamos el cursor si es el jugador que usa Teclado/Mouse
        // Si usan mandos (Gamepads), no hace falta bloquear el cursor.
    }

    void Update()
    {
        Move();
    }

    // Este evento es llamado dinámicamente por el componente "Player Input" de cada jugador
    public void OnLook(InputValue value)
    {
        mira = value.Get<Vector2>();
    }

    public void Move()
    {
        // Funciona tanto con Stick Derecho del mando como con el Delta del Mouse
        float lookX = mira.x * sensibilidad * Time.deltaTime;
        float lookY = mira.y * sensibilidad * Time.deltaTime;

        rotacionX -= lookY;
        rotacionX = Mathf.Clamp(rotacionX, -90f, 90f);

        if (camara != null)
        {
            camara.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);
            transform.Rotate(Vector3.up * lookX);
        }
    }
}