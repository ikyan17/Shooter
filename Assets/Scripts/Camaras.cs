using UnityEngine;
using System.Collections;

public class Camaras : MonoBehaviour
{

    [SerializeField] private GameObject camera1;
    [SerializeField] private GameObject camera2;
   


    [SerializeField] private float tiempoPorCamara = 2.0f;


    private int camaraActual = 1;
    private bool ejecutandoSecuencia = false;

    void Start()
    {

        ActualizarCamaras();
    }

    void OnTriggerEnter(Collider collision)
    {

        if (collision.CompareTag("Player") && !ejecutandoSecuencia)
        {
            StartCoroutine(SecuenciaDeCamaras());
        }
    }


    private IEnumerator SecuenciaDeCamaras()
    {
        ejecutandoSecuencia = true;


        camaraActual = 1;
        ActualizarCamaras();
        yield return new WaitForSeconds(tiempoPorCamara);


        camaraActual = 2;
        ActualizarCamaras();
        yield return new WaitForSeconds(tiempoPorCamara);


       

        camaraActual = 1;
        ActualizarCamaras();

        ejecutandoSecuencia = false;
    }


    private void ActualizarCamaras()
    {
        camera1.SetActive(camaraActual == 1);
        camera2.SetActive(camaraActual == 2);
        
    }
}