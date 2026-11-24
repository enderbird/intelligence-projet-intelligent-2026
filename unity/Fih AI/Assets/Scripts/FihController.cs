using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class FihController : MonoBehaviour
{
    /* - - - - - - - - - - - - - - - - - - - - */
    [Header("Movements")]
    private Vector2 m_moveInput;      // WASD (X = left/right, Y = forward/back)
    private float m_elevationInput;   // Space / Shift (up/down)

    public GameObject m_freelookCamera;
    private CinemachineCamera m_freelookCamera_cinemachine;

    /* - - - - - - - - - - - - - - - - - - - - */
    private Rigidbody m_rigidbody;

    public float m_quoefficientRecentrement = 4f;
    public float m_forceRecentrement = 0.6f;

    public float m_cameraRecentrement = 5f;

    private InputAction m_moveAction;
    private InputAction m_elevationAction;
    /* - - - - - - - - - - - - - - - - - - - - */

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.useGravity = true; // Fish floats in 3D water

        m_moveAction = InputSystem.actions.FindAction("Move");
        m_elevationAction = InputSystem.actions.FindAction("Elevation");

        m_freelookCamera_cinemachine = m_freelookCamera.GetComponent<CinemachineCamera>();
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


        Vector3 worldMove = transform.TransformDirection(direction);
        m_rigidbody.linearVelocity += worldMove;

        m_rigidbody.linearVelocity = Vector3.ClampMagnitude(m_rigidbody.linearVelocity, 5);


        // World space rotation in degrees (0 to 360)
        Vector3 currentTilt = m_rigidbody.transform.eulerAngles;

        float t = 1f - Mathf.Exp(-m_quoefficientRecentrement * Time.deltaTime);

        Vector3 newEuler = new Vector3(
            Mathf.LerpAngle(currentTilt.x, 0, t * m_forceRecentrement),
            Mathf.LerpAngle(currentTilt.y, m_freelookCamera.transform.eulerAngles.y, t * m_cameraRecentrement),
            Mathf.LerpAngle(currentTilt.z, 0, t * m_forceRecentrement)
        );
        float newEulerCameraZ = Mathf.LerpAngle(currentTilt.z, m_freelookCamera.transform.eulerAngles.z, t * m_cameraRecentrement);

        m_rigidbody.transform.eulerAngles = newEuler;
        m_freelookCamera_cinemachine.Lens.Dutch = newEulerCameraZ;
    }

    public void Die()
    {
        GetComponent<BoxCollider>().enabled = false;
        enabled = false;
    }

}