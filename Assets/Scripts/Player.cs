using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class Player : Character
{
    
    public static Player instancia;

    [SerializeField] private TextMeshProUGUI vidasText;
    [SerializeField] private TextMeshProUGUI textoContador;
    [HideInInspector] public int contador = 0;

    public Transform spawnPoint;
    public GameObject bullet;
    public float shotForce = 1500f;
    public float shotRate = 0.5f;
    public float lifeTime = 3f;
    private float nextShotTime;

    [SerializeField] private float sensibilidadMouse;
    private Vector2 mira;


    void Awake()
    {
      
        instancia = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

      
        m_vidaActual = m_vida;
        Debug.Log(m_vidaActual);
        ActualizarTexto();
        ActualizarTextoContador(); 

       
    }

    void FixedUpdate()
    {
        Move();
        Die();
    }

   
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage); 
        ActualizarTexto();       
    }

    public void OnMove(InputValue inputValue)
    {
        inputMove = inputValue.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && EstaEnSuelo())
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, fuerzaSalto, rb.linearVelocity.z);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        
        if (collider.gameObject.CompareTag("Trampa"))
        {
            DamagePlayer(10);
            
        }
    }

    void ActualizarTexto()
    {
        
        {
            vidasText.text = "Vidas: " + m_vidaActual;
        }
    }

    
    public void ActualizarTextoContador()
    {
        
        {
            textoContador.text = "Trampas: " + contador;
        }
    }



    public void OnAttack(InputValue value)
    {
        if (value.isPressed && Time.time >= nextShotTime)
        {
            nextShotTime = Time.time + shotRate;

            GameObject newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotForce);


            StartCoroutine(DestroyBullet(newBullet));
        }
    }


    private IEnumerator DestroyBullet(GameObject Bullet)
        
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(Bullet);
    }


  

    
    
  


}
