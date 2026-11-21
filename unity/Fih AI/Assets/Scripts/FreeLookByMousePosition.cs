using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineOrbitalFollow))]
public class FreeLookByMousePosition : MonoBehaviour
{
    /* - - - - - - - - - - - - - - - - - - - - */
    private CinemachineOrbitalFollow m_cinemachineOrbitalFollow;

    private void Awake()
    {
        m_cinemachineOrbitalFollow = GetComponent<CinemachineOrbitalFollow>();
    }

    private void Update()
    {
        //if (freeLook == null) return;

        //float nx = Mathf.Clamp01(Input.mousePosition.x / Screen.width);
        //float ny = Mathf.Clamp01(Input.mousePosition.y / Screen.height);

        //freeLook.m_XAxis.Value = Mathf.Lerp(-180f, 180f, nx);
        //freeLook.m_YAxis.Value = ny;
    }
}
