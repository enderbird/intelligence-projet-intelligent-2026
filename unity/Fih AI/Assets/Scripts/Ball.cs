using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class Ball : MonoBehaviour
{
    public float m_distanceMort = 40;
    public float m_friction = 1f;

    public Vector3 m_direction;
    public float m_acceleration = 5;

    private Rigidbody m_rigidbody;
    private SphereCollider m_collider;


    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_collider = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        PhysicsMaterial t_materiel = new PhysicsMaterial();
        t_materiel.dynamicFriction = m_friction; // Friction en mouvement
        t_materiel.staticFriction = m_friction;  // Friction à l'arrêt

        m_collider.material = t_materiel;
    }

    void Update()
    {
        // il die
        if (transform.position.sqrMagnitude > m_distanceMort * m_distanceMort)
            Die();
    }
    void FixedUpdate()
    {
        Vector3 t_nouvelleDirection = Quaternion.Euler(m_direction) * Vector3.forward;
        m_rigidbody.AddForce(t_nouvelleDirection * m_acceleration);
    }

    public void Die()
    {
        GetComponent<SphereCollider>().enabled = false;
        enabled = false;
        Destroy(gameObject);
    }
}
