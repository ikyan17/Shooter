using UnityEngine;
using UnityEngine.InputSystem; // Necesario para el nuevo Input System

public class LocalPlayerJoinHandler : MonoBehaviour
{
    // Este método se ejecuta automáticamente cada vez que un nuevo jugador se une presionando Start/un botón
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        // 1. Obtenemos el GameObject del jugador que acaba de unirse
        GameObject nuevoJugador = playerInput.gameObject;

        // 2. Aplicamos tu lógica de cambio de color
        ChangeColor(nuevoJugador, Random.ColorHSV());

        // Opcional: Podrías asignarle una posición de inicio basada en cuántos jugadores van uniéndose
        // nuevoJugador.transform.position = ObtenerPosicionDeSpawn(playerInput.playerIndex);
    }

    void ChangeColor(GameObject obj, Color newColor)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = newColor;
        }
    }
}