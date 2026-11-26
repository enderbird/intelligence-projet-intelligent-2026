
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System;

[RequireComponent(typeof(Rigidbody))]
public class FihAgent : Agent
{
    public event EventHandler OnAteFood;
    public event EventHandler OnEpisodeBeginEvent;

    [SerializeField] private FoodSpawner foodSpawner;
    [SerializeField] private FoodButton foodButton;

    [Header("Movement")]
    [SerializeField] private float m_moveSpeed = 5f;

    [Header("Rewards")]
    [SerializeField] private float m_buttonReward = 0.2f;
    [SerializeField] private float m_foodReward = 1f;
    [SerializeField] private float m_stepPenalty = -0.001f;
    [SerializeField] private float m_invalidButtonPenalty = -0.05f;

    private Rigidbody agentRigidbody;

    // Devrait être dans Awake mais fuckery
    private void Start() {
        agentRigidbody = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        // Reset movement
        agentRigidbody.linearVelocity = Vector3.zero;
        agentRigidbody.angularVelocity = Vector3.zero;

        // Reset position
        transform.localPosition = new Vector3(
            UnityEngine.Random.Range(-2.5f, 2.5f),
            0f,
            UnityEngine.Random.Range(-2f, 2f)
        );

        // Reset environment
        foodSpawner.ResetSpawner();
        foodButton.ResetButton();

        OnEpisodeBeginEvent?.Invoke(this, EventArgs.Empty);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Button availability
        sensor.AddObservation(foodButton.CanUseButton() ? 1f : 0f);

        // Direction toward button
        Vector3 dirToButton = (foodButton.transform.position - transform.position).normalized;
        sensor.AddObservation(dirToButton);

        // Food availability
        bool hasFood = foodSpawner.HasFoodSpawned();
        sensor.AddObservation(hasFood ? 1f : 0f);

        // Direction toward food
        if (hasFood) {
            Vector3 dirToFood = (foodSpawner.GetLastFoodTransform().position - transform.position).normalized;
            sensor.AddObservation(dirToFood);
        }
        else {
            sensor.AddObservation(Vector3.zero);
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        ActionSegment<int> discreteActions = actions.DiscreteActions;

        // Movement actions
        int moveX = discreteActions[0];
        int moveY = discreteActions[1];
        int moveZ = discreteActions[2];

        Vector3 movement = new Vector3(
            moveX - 1,
            moveY - 1,
            moveZ - 1
        );

        // Normalize diagonal movement
        movement = Vector3.ClampMagnitude(movement, 1f);

        agentRigidbody.linearVelocity = movement * m_moveSpeed;

        // Button action
        bool useButton = discreteActions[3] == 1;
        if (useButton) {
            TryUseButton();
        }

        // Time penalty
        AddReward(m_stepPenalty);
    }

    private void TryUseButton()
    {
        // No reward if button is already unavailable
        if (!foodButton.CanUseButton())
        {
            AddReward(m_invalidButtonPenalty);
            return;
        }

        // Check whether agent is close enough
        Collider[] colliders = Physics.OverlapBox(
            transform.position,
            Vector3.one * 0.5f
        );

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<FoodButton>(out FoodButton detectedButton) && detectedButton == foodButton) {
                detectedButton.UseButton();

                AddReward(m_buttonReward);
                return;
            }
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut) {
        ActionSegment<int> actions = actionsOut.DiscreteActions;

        // X: Left / None / Right
        actions[0] = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")) + 1;
        // Y: Down / None / Up
        actions[1] = Mathf.RoundToInt(Input.GetAxisRaw("Elevation")) + 1;
        // Z: Backward / None / Forward
        actions[2] = Mathf.RoundToInt(Input.GetAxisRaw("Vertical")) + 1;

        // Button
        actions[3] = Input.GetKey(KeyCode.E) ? 1 : 0;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.TryGetComponent<Food>(out Food food)) {
            AddReward(m_foodReward);
            OnAteFood?.Invoke(this, EventArgs.Empty);
            Destroy(food.gameObject);

            EndEpisode();
        }
    }
}
