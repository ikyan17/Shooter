using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LineRenderer))]
public class GanchoAgarre : MonoBehaviour
{
    private Camera camaraJugador;
    private Transform puntaDelArma;

    [Header("Configuración del Gancho")]
    [SerializeField] private LayerMask capasEnganchables;
    [SerializeField] private float distanciaMaxima = 50f;
    [SerializeField] private float fuerzaTiron = 4.5f;
    [SerializeField] private float amortiguacion = 7f;

    private SpringJoint resorte;
    private Vector3 puntoDeEnganche;
    private LineRenderer cuerdaVisual;
    private bool estaEnganchado = false;

    void Awake()
    {
        cuerdaVisual = GetComponent<LineRenderer>();
        cuerdaVisual.positionCount = 2;
        cuerdaVisual.enabled = false;

        // Auto-detecta la cámara que está dentro de ESTE jugador específico
        camaraJugador = GetComponentInChildren<Camera>();
        if (camaraJugador == null)
        {
            Debug.LogError("No se encontró ninguna cámara en los hijos de este jugador.");
        }

        // Auto-detecta o crea un punto de referencia para la cuerda
        Transform hijoPunta = transform.Find("PuntaGancho");
        if (hijoPunta != null)
        {
            puntaDelArma = hijoPunta;
        }
        else if (camaraJugador != null)
        {
            puntaDelArma = camaraJugador.transform; // Si no hay, sale desde la cámara
        }
        else
        {
            puntaDelArma = transform;
        }
    }

    public void OnGrapple(InputValue value)
    {
        if (value.isPressed)
        {
            if (!estaEnganchado)
            {
                LanzarGancho();
            }
            else
            {
                SoltarGancho();
            }
        }
    }

    private void LanzarGancho()
    {
        if (camaraJugador == null) return;

        // El centro de la pantalla es independiente para cada cámara en pantalla partida
        Vector3 centroPantalla = new Vector3(0.5f, 0.5f, 0);
        Ray rayoDesdeCamara = camaraJugador.ViewportPointToRay(centroPantalla);
        RaycastHit impacto;

        if (Physics.Raycast(rayoDesdeCamara, out impacto, distanciaMaxima, capasEnganchables))
        {
            puntoDeEnganche = impacto.point;

            resorte = gameObject.AddComponent<SpringJoint>();
            resorte.autoConfigureConnectedAnchor = false;
            resorte.connectedAnchor = puntoDeEnganche;

            float distanciaAlPunto = Vector3.Distance(transform.position, puntoDeEnganche);
            resorte.maxDistance = distanciaAlPunto * 0.8f;
            resorte.minDistance = distanciaAlPunto * 0.25f;

            resorte.spring = fuerzaTiron;
            resorte.damper = amortiguacion;
            resorte.massScale = 4.5f;

            cuerdaVisual.enabled = true;
            estaEnganchado = true;
        }
    }

    private void SoltarGancho()
    {
        cuerdaVisual.enabled = false;
        estaEnganchado = false;

        if (resorte != null)
        {
            Destroy(resorte);
        }
    }

    void LateUpdate()
    {
        if (cuerdaVisual.enabled && puntaDelArma != null)
        {
            cuerdaVisual.SetPosition(0, puntaDelArma.position);
            cuerdaVisual.SetPosition(1, puntoDeEnganche);
        }
    }
}