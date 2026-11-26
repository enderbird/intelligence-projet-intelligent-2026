
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [Header("Spawner")]
    [SerializeField] protected GameObject m_prefabObject;

    protected float m_timer = 0f;

    protected virtual void Awake() {
        if (m_prefabObject == null)
        {
            Debug.LogError($"{name}: No prefab assigned to Spawner!", this);
            enabled = false;
        }
    }

    protected virtual void Update() {
        m_timer += Time.deltaTime;
    }

    public virtual void ResetSpawner(){ m_timer = 0f; }

    protected abstract void Spawn();
}
