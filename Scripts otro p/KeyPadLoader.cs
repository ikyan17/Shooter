using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class KeypadLevelLoader : MonoBehaviour
{
    [Header("UI & Referencias")]
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string correctCode;
    [SerializeField] private GameObject panelKeyPad; // Panel del KeyPad en la UI
    [SerializeField] private MonoBehaviour playerMovementScript; // Script de movimiento del jugador

    private bool keypadIsActive = false;

    // Detecta la colisión con el objeto en 3D
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !keypadIsActive)
        {
            if (playerMovementScript == null)
            {
                playerMovementScript = other.GetComponent<MonoBehaviour>();
            }

            OpenKeypad();
        }
    }

    private void OpenKeypad()
    {
        keypadIsActive = true;
        panelKeyPad.SetActive(true);
        text.text = "";

        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = false; // Desactiva el movimiento del jugador
        }
    }

    // Método para los botones numéricos
    public void Number(int number)
    {
        text.text += number.ToString();
    }

    // Valida la contraseña introducida
    public void VerifyCode()
    {
        if (text.text == correctCode)
        {
            Debug.Log("Contraseña Correcta - Cambiando de nivel");
            CloseKeypad();

            // Carga la siguiente escena en el Build Settings
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            Debug.Log("Contraseña Incorrecta");
            text.text = "";
        }
    }

    // Permite cerrar el pad si el jugador decide cancelar
    public void CloseKeypad()
    {
        panelKeyPad.SetActive(false);
        keypadIsActive = false;

        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = true; // Reactiva el movimiento del jugador
        }
    }
}