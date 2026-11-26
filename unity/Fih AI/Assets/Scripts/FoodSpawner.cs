
using UnityEngine;

public class FoodSpawner : Spawner
{
    private GameObject m_lastFoodObject;

    protected override void Spawn() {
        // Don't spawn another food if one already exists
        if (HasFoodSpawned()) return;

        GameObject spawnedObject = Instantiate(
            m_prefabObject,
            transform.position,
            Quaternion.identity
        );

        m_lastFoodObject = spawnedObject;
    }

    public bool HasFoodSpawned() { return m_lastFoodObject != null; }

    public Transform GetLastFoodTransform() {
        return m_lastFoodObject != null ? m_lastFoodObject.transform : null;
    }

    public override void ResetSpawner() {
        base.ResetSpawner();

        if (m_lastFoodObject != null) {
            Destroy(m_lastFoodObject);
            m_lastFoodObject = null;
        }
    }
}
