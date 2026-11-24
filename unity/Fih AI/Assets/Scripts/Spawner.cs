using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Spawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject m_prefabObject;

    public Vector3 m_direction = new Vector3(1, 0, 0);

    private float m_timer = 0f;
    public float m_spawnRate = 1f;

    private void Awake()
    {
        if (m_prefabObject == null) enabled = false; 
    }
    void Update()
    {
        m_timer += Time.deltaTime;

        if (m_timer >= m_spawnRate)
        {
            Spawn();
            m_timer = 0f;
        }
    }
    public void Spawn()
    {
        GameObject spawnedObject = Instantiate(
            m_prefabObject,
            transform.position,
            Quaternion.Euler(m_direction)
        );

        Ball t_scriptBall = spawnedObject.GetComponent<Ball>();
        if (t_scriptBall)
        {
            t_scriptBall.m_direction = m_direction;
        }

    }
}
