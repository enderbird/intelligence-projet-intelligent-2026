using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class FihController : MonoBehaviour
{
    /* - - - - - - - - - - - - - - - - - - - - */
    [Header("Movements")]
    private Vector2 m_moveInput;      // WASD (X = left/right, Y = forward/back)
    private float m_elevationInput;   // Space / Shift (up/down)

    /* - - - - - - - - - - - - - - - - - - - - */
    private Rigidbody m_rigidbody;

    private InputAction m_moveAction;
    private InputAction m_elevationAction;
    /* - - - - - - - - - - - - - - - - - - - - */

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.useGravity = false; // Fish floats in 3D water

        m_moveAction = InputSystem.actions.FindAction("Move");
        m_elevationAction = InputSystem.actions.FindAction("Elevation");
    }

    private void Update()
    {
        /* - - - - - WASD - - - - - */
        m_moveInput = m_moveAction.ReadValue<Vector2>();
        m_elevationInput = m_elevationAction.ReadValue<float>();
    }

    private void FixedUpdate()
    {
        // Map WASD to X/Z and Space/Shift to Y
        Vector3 direction = new Vector3(m_moveInput.x, m_elevationInput, m_moveInput.y);
        if (direction.sqrMagnitude > 1f) direction.Normalize();

        // Move relative to where the fish is facing (use direction directly for world space)

        Vector3 worldMove = transform.TransformDirection(direction);
        m_rigidbody.linearVelocity += worldMove;

        m_rigidbody.linearVelocity = Vector3.ClampMagnitude(m_rigidbody.linearVelocity, 4);
    }

    public void Die()
    {
        GetComponent<CapsuleCollider>().enabled = false;

        enabled = false;
    }

}