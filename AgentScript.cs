using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.Sentis;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class AgentScript : Agent
{
    private Rigidbody _rigidbody;
    public TextMeshProUGUI _text;

    public List<Transform> targets;
    public Transform bomb;

    private float _magnitude = 1f;
    private int targetsCollected = 0;

    private ScoreManager scoreManager;

    public UpgradeController upgradeController;

    private GameObject games;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();       
        upgradeController = GameObject.FindGameObjectWithTag("UpgradeController").GetComponent<UpgradeController>();
        _magnitude = upgradeController.speed;
        Debug.Log("mag = " + _magnitude);
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    public override void OnEpisodeBegin()
    {
        // Reset agent position and velocity
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.velocity = Vector3.zero;
        transform.localPosition = new Vector3(0f, 0.5f, 0f);

        bomb.localPosition = new Vector3(Random.value * 8f - 4f, 0.5f, Random.value * 8f - 4f);

        targetsCollected = 0;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Add target and bomb positions as observations
        foreach (var target in targets)
        {
            sensor.AddObservation(target.localPosition);
        }
        sensor.AddObservation(bomb.localPosition);

        // Add agent position and velocity as observations
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(_rigidbody.velocity.x);
        sensor.AddObservation(_rigidbody.velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actions.ContinuousActions[0];
        controlSignal.z = actions.ContinuousActions[1];


        _rigidbody.AddForce(controlSignal * _magnitude);


        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, 5f);


        for (int i = 0; i < targets.Count; i++)
        {
            if (Vector3.Distance(transform.localPosition, targets[i].localPosition) < 1.4f)
            {
                ScoreManager.scoreCount++;
                targetsCollected++;
               // targets[i].localPosition = new Vector3(Random.value * 8f - 4f, 0.5f, Random.value * 8f - 4f);
                 targets[i].localPosition = new Vector3(0, -50, Random.value * 8f - 4f);

                _rigidbody.velocity = Vector3.zero;


                SetReward(1.0f);
                if (targetsCollected == targets.Count)
                {
                    EndEpisode();
                    SceneManager.LoadScene(4);

                }
                return;
            }
        }


        if (Vector3.Distance(transform.localPosition, bomb.localPosition) < 1.42f)
        {
            SetReward(-1.0f);
        }


        if (transform.localPosition.y < 0)
        {
            SetReward(-1.0f);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }


    private Transform FindClosestTarget()
    {
        Transform closestTarget = null;
        float minDistance = float.MaxValue;

        foreach (var target in targets)
        {
            float distance = Vector3.Distance(transform.localPosition, target.localPosition);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestTarget = target;
            }
        }

        return closestTarget;
    }

    private void Update()
    {
        if (targetsCollected < targets.Count)
        {
            Transform closestTarget = FindClosestTarget();
            Vector3 direction = (closestTarget.localPosition - transform.localPosition).normalized;
            _rigidbody.AddForce(direction * _magnitude);
        }
    }
}
