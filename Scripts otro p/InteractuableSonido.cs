using UnityEngine;
using System.Collections;

public class SecuenciaConSonido : MonoBehaviour
{
    [Header("Lista de Cámaras de la Secuencia")]
    [SerializeField] private GameObject camera1;
    [SerializeField] private GameObject camera2;
    [SerializeField] private GameObject camera3;
    [SerializeField] private GameObject camera4;
    [SerializeField] private GameObject camera5;

    [Header("Configuración del Recorrido")]
    [SerializeField] private float tiempoPorCamara = 2.0f; // Tiempo que dura cada vista

    // Variables de control
    private int camaraActual = 1;
    private bool ejecutandoSecuencia = false;

    // Referencia al componente de audio
    private AudioSource audioSource;

    void Start()
    {
        // Obtenemos el AudioSource que debe estar en este mismo objeto
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider collision)
    {
        // Verificamos que sea un jugador y que no haya una secuencia corriendo
        if (collision.CompareTag("Player") && !ejecutandoSecuencia)
        {
            // Buscamos la cámara específica que pertenece al jugador que entró al trigger
            // (Busca un componente Camera dentro de los hijos del GameObject del jugador)
            Camera camaraDelJugador = collision.GetComponentInChildren<Camera>();

            if (camaraDelJugador != null)
            {
                ReproducirSonido();
                StartCoroutine(SecuenciaDeCamaras(camaraDelJugador.gameObject));
            }
            else
            {
                Debug.LogWarning("El jugador que entró al trigger no tiene un componente Camera asignado en sus hijos.");
            }
        }
    }

    // Corrutina que recibe específicamente la cámara del jugador que activó el trigger
    private IEnumerator SecuenciaDeCamaras(GameObject mainCameraJugador)
    {
        ejecutandoSecuencia = true;

        // Apagamos la cámara principal de ESTE jugador en específico al iniciar
        if (mainCameraJugador != null)
        {
            mainCameraJugador.SetActive(false);
        }

        // --- PUNTO DE VISTA 1 ---
        camaraActual = 1;
        ActualizarCamarasSecuencia();
        yield return new WaitForSeconds(tiempoPorCamara);

        // --- PUNTO DE VISTA 2 ---
        camaraActual = 2;
        ActualizarCamarasSecuencia();
        yield return new WaitForSeconds(tiempoPorCamara);

        // --- PUNTO DE VISTA 3 ---
        camaraActual = 3;
        ActualizarCamarasSecuencia();
        yield return new WaitForSeconds(tiempoPorCamara);

        // --- PUNTO DE VISTA 4 ---
        camaraActual = 4;
        ActualizarCamarasSecuencia();
        yield return new WaitForSeconds(tiempoPorCamara);

        // --- PUNTO DE VISTA 5 ---
        camaraActual = 5;
        ActualizarCamarasSecuencia();
        yield return new WaitForSeconds(tiempoPorCamara);

        // --- FIN DE LA SECUENCIA ---
        camaraActual = 0;
        ActualizarCamarasSecuencia();

        // Regresamos la cámara únicamente al jugador exacto que inició esta corrutina
        if (mainCameraJugador != null)
        {
            mainCameraJugador.SetActive(true);
        }

        ejecutandoSecuencia = false;
    }

    // Método para apagar las cámaras de la cinemática
    private void ActualizarCamarasSecuencia()
    {
        if (camera1 != null) camera1.SetActive(camaraActual == 1);
        if (camera2 != null) camera2.SetActive(camaraActual == 2);
        if (camera3 != null) camera3.SetActive(camaraActual == 3);
        if (camera4 != null) camera4.SetActive(camaraActual == 4);
        if (camera5 != null) camera5.SetActive(camaraActual == 5);
    }

    // Método para manejar el sonido de forma segura
    void ReproducirSonido()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}