using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class FihController : MonoBehaviour
{
    /* - - - - - - - - - - - - - - - - - - - - */
    [Header("Movements")]
    private Vector2 m_moveInput;      // WASD (X = left/right, Y = forward/back)
    private float m_elevationInput;   // Space / Shift (up/down)

    public Transform m_mainCamera;

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


        // World space rotation in degrees (0 to 360)
        Vector3 currentTilt = m_rigidbody.transform.eulerAngles;
        //float xTilt = currentTilt.x; // Pitch (tilting forward/backward)
        //float yRotation = currentTilt.y; // Yaw (turning left/right)
        //float zTilt = currentTilt.z; // Roll (tilting side-to-side)
        //Debug.Log("Tilt:" + xTilt);
        //Debug.Log("Rotation:" + yRotation);
        //Debug.Log("Tilt:" + zTilt);

        float t = 1f - Mathf.Exp(-4f * Time.deltaTime);
        Vector3 snappedTarget = new Vector3(
            Mathf.Round(currentTilt.x / 90f) * 90f,
            Mathf.Round(currentTilt.y / 90f) * 90f,
            Mathf.Round(currentTilt.z / 90f) * 90f
        );

        Vector3 newEuler = new Vector3(
            Mathf.LerpAngle(currentTilt.x, 0, t),
            Mathf.LerpAngle(currentTilt.y, m_mainCamera.transform.eulerAngles.y, t),
            Mathf.LerpAngle(currentTilt.z, 0, t)
        );

        m_rigidbody.transform.eulerAngles = newEuler;
    }

    public void Die()
    {
        GetComponent<CapsuleCollider>().enabled = false;

        enabled = false;
    }

}