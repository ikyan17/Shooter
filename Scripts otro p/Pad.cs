using UnityEngine;
using TMPro;

public class Pad : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string code;
    [SerializeField] private GameObject panelKeyPad; // Panel del KeyPad en la UI
    [SerializeField] private Player player; // Referencia al Player

    public void ActivatePad()
    {
        panelKeyPad.SetActive(true);
        text.text = "";
        player.enabled = false; // Desactiva el movimiento del Player
    }

    public void Number(int number)
    {
        text.text += number.ToString();
    }

    public void CodeValue()
    {
        if (text.text == code)
        {
            Debug.Log("Contraseña Correcta");
            panelKeyPad.SetActive(false); // Oculta el KeyPad
            player.enabled = true;        // Reactiva el movimiento del Player
        }
        else
        {
            Debug.Log("Contraseña Incorrecta");
            text.text = "";
        }
    }

    public void ClosePad()
    {
        panelKeyPad.SetActive(false);
        player.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivatePad();
        }
    }
}
