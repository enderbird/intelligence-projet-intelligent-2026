using Unity.Cinemachine;
using UnityEngine;

public class FreeLookByMousePosition : MonoBehaviour
{
    public CinemachineOrbitalFollow cinemachineOrbitalFollow;

    void Update()
    {
        //if (freeLook == null) return;

        //float nx = Mathf.Clamp01(Input.mousePosition.x / Screen.width);
        //float ny = Mathf.Clamp01(Input.mousePosition.y / Screen.height);

        //freeLook.m_XAxis.Value = Mathf.Lerp(-180f, 180f, nx);
        //freeLook.m_YAxis.Value = ny;
    }
}
