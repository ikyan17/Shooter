using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Transform laserTag;

    [SerializeField] private float vel;
    [SerializeField] public float fuerzaSalto;
    [SerializeField] protected float m_vida;

    protected float m_vidaActual;
    protected Vector2 inputMove;
    protected Rigidbody rb;

    public void Move()
    {
        Vector3 direction = new Vector3(inputMove.x, 0, inputMove.y);
        transform.Translate(direction * vel * Time.deltaTime);
    }

    public bool EstaEnSuelo()
    {
        RaycastHit hit;

        if (Physics.Raycast(laserTag.position, Vector3.down, out hit, 0.5f))
        {
            return hit.collider.CompareTag("Suelo");
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(laserTag.position, laserTag.position + Vector3.down * 0.5f);
    }


    public virtual void TakeDamage(float damage)
    {
        m_vidaActual -= damage;
        Debug.Log("Vida actual: " + m_vidaActual);
    }

    public void DamagePlayer(float damage)
    {
        TakeDamage(damage);
    }

    public void Die()
    {
        if (m_vidaActual <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }



}