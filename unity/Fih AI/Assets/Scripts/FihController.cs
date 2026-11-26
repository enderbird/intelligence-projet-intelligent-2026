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

    public float m_cameraRecentrementHorizontal = 5f;
    public float m_cameraRecentrementVertical = 5f;
    public float m_cameraRecentrementLateral = 1f;

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
        // 1. Movement
        // Map WASD to X/Z and Space/Shift to Y
        Vector3 direction = new Vector3(m_moveInput.x, m_elevationInput, m_moveInput.y);
        if (direction.sqrMagnitude > 1f) direction.Normalize();

        Vector3 worldMove = transform.TransformDirection(direction);
        m_rigidbody.linearVelocity = Vector3.ClampMagnitude(m_rigidbody.linearVelocity + worldMove, 5f);

        // 2. Smooth Yaw (Y) toward camera, while leveling Pitch (X) and Roll (Z) to 0
        Quaternion currentRot = m_rigidbody.rotation;
        Quaternion targetYaw = Quaternion.Euler(0f, m_freelookCamera.transform.eulerAngles.y, 0f);

        // Keep multiplier inside Exp() so 't' never exceeds 1.0f
        float tYaw = 1f - Mathf.Exp(-m_quoefficientRecentrement * m_cameraRecentrementVertical * Time.fixedDeltaTime);
        float tLevel = 1f - Mathf.Exp(-m_quoefficientRecentrement * m_forceRecentrement * Time.fixedDeltaTime);

        // Slerp yaw toward camera, and level out pitch/roll smoothly
        Quaternion blendedYaw = Quaternion.Slerp(currentRot, targetYaw, tYaw);
        Quaternion finalRot = Quaternion.Slerp(blendedYaw, Quaternion.Euler(0f, blendedYaw.eulerAngles.y, 0f), tLevel);

        m_rigidbody.MoveRotation(finalRot);

        // 3. Smooth Camera Dutch (Roll)
        float currentDutch = m_freelookCamera_cinemachine.Lens.Dutch;
        float targetDutch = Mathf.DeltaAngle(0f, m_freelookCamera.transform.eulerAngles.z);
        float tDutch = 1f - Mathf.Exp(-m_quoefficientRecentrement * m_cameraRecentrementLateral * Time.fixedDeltaTime);

        m_freelookCamera_cinemachine.Lens.Dutch = Mathf.Lerp(currentDutch, targetDutch, tDutch);
    }

    public void Die()
    {
        GetComponent<BoxCollider>().enabled = false;
        enabled = false;
    }

}