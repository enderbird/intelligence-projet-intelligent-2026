using UnityEngine;

public class ProjectileSpawner : Spawner
{
    public Vector3 m_direction = new Vector3(1, 0, 0);
    [SerializeField] protected float m_spawnRate = 1f;

    protected override void Update() {
        base.Update();

        if (m_timer >= m_spawnRate)
        {
            Spawn();
            m_timer = 0f;
        }
    }

    protected override void Spawn() {
        Quaternion rotation = m_direction.sqrMagnitude > 0f
            ? Quaternion.LookRotation(m_direction.normalized)
            : Quaternion.identity;

        GameObject spawnedObject = Instantiate(
            m_prefabObject,
            transform.position,
            rotation
        );

        Ball ball = spawnedObject.GetComponent<Ball>();
        if (ball != null) {
            ball.m_direction = m_direction;
        }
    }
}
