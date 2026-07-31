using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerDetection))]
public class EnemyMovement : MonoBehaviour
{
    private enum State
    {
        Patrolling,
        Chasing,
        Investigating
    }

    [Header("Patrol")]
    [SerializeField]
    private Transform pointA;

    [SerializeField]
    private Transform pointB;

    [Header("Targets")]
    [SerializeField]
    private Transform player;

    [Header("Movement")]
    [SerializeField, Min(0f)]
    private float movementSpeed = 2f;

    [SerializeField, Min(0f)]
    private float rotationSpeed = 360f;

    [SerializeField, Min(0f)]
    private float arrivalDistance = 0.05f;

    [Header("Investigation")]
    [SerializeField, Min(0f)]
    private float investigationDuration = 3f;

    // Component dependencies
    private NavMeshAgent agent;
    private PlayerDetection playerDetection;

    // Runtime state
    private State state;
    private Transform patrolTarget;
    private Vector3 lastKnownPlayerPosition;
    private float investigationTimer;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerDetection = GetComponent<PlayerDetection>();

        agent.speed = movementSpeed;
        agent.angularSpeed = rotationSpeed;
        agent.stoppingDistance = arrivalDistance;
    }

    private void Start()
    {
        state = State.Patrolling;
        SetPatrolTarget(pointA);
    }

    private void Update()
    {
        switch (state)
        {
            case State.Patrolling:
                UpdatePatrolling();
                break;

            case State.Chasing:
                UpdateChasing();
                break;

            case State.Investigating:
                UpdateInvestigating();
                break;
        }
    }

    private void UpdatePatrolling()
    {
        if (playerDetection.IsPlayerDetected)
        {
            EnterChasingState();
            return;
        }

        if (agent.pathPending)
        {
            return;
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Transform nextTarget =
                patrolTarget == pointA ? pointB : pointA;

            SetPatrolTarget(nextTarget);
        }
    }

    private void UpdateChasing()
    {
        if (!playerDetection.IsPlayerDetected)
        {
            EnterInvestigatingState();
            return;
        }

        lastKnownPlayerPosition = player.position;
        agent.SetDestination(lastKnownPlayerPosition);
    }

    private void EnterChasingState()
    {
        state = State.Chasing;
        Debug.Log("Enemy detected player, chasing");
        lastKnownPlayerPosition = player.position;
        agent.SetDestination(lastKnownPlayerPosition);
    }

    private void EnterPatrollingState()
    {
        state = State.Patrolling;
        Debug.Log("Enemy lost player, returning to patrol");
        agent.SetDestination(patrolTarget.position);
    }

    private void SetPatrolTarget(Transform newTarget)
    {
        patrolTarget = newTarget;
        agent.SetDestination(patrolTarget.position);
    }

    private void EnterInvestigatingState()
    {
        state = State.Investigating;
        investigationTimer = 0f;

        agent.SetDestination(lastKnownPlayerPosition);

        Debug.Log("Enemy entered investigating state");
    }

    private void UpdateInvestigating()
    {
        if (playerDetection.IsPlayerDetected)
        {
            EnterChasingState();
            return;
        }

        if (agent.pathPending)
        {
            return;
        }

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            return;
        }

        investigationTimer += Time.deltaTime;

        if (investigationTimer >= investigationDuration)
        {
            EnterPatrollingState();
        }
    }
}
