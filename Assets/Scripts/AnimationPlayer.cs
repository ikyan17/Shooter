using UnityEngine;
// No olvides importar la librería del nuevo sistema de input
using UnityEngine.InputSystem;

public class ControlPersonaje : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

   
    private void OnJump(InputValue value)
    {
     
        if (value.isPressed)
        {
            LogicaDelSalto();
        }
    }

    void LogicaDelSalto()
    {
       
        animator.SetTrigger("Saltar");
    }
}