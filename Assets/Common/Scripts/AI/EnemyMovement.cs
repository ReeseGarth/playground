using System;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Patrolling,
    Chasing,
    Investigating,
    Scanning
}

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerDetection))]
public class EnemyMovement : MonoBehaviour
{
    [Header("Patrol")]
    [SerializeField]
    private Transform[] patrolPoints;

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

    [SerializeField, Min(0f)]
    private float investigationRotationSpeed = 120f;

    // Component dependencies
    private NavMeshAgent agent;
    private PlayerDetection playerDetection;

    // Runtime state
    public EnemyState CurrentState { get; private set; }
    public event Action<EnemyState> StateChanged;
    private Transform patrolTarget;
    private Vector3 lastKnownPlayerPosition;
    private float investigationTimer;
    private int patrolPointIndex;

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
        if (patrolPoints.Length == 0)
        {
            Debug.LogError(
                "Enemy requires at least one patrol point.",
                this
            );

            enabled = false;
            return;
        }

        ChangeState(EnemyState.Patrolling);

        patrolPointIndex = 0;
        SetPatrolTarget(patrolPoints[patrolPointIndex]);
    }

    private void Update()
    {
        switch (CurrentState)
        {
            case EnemyState.Patrolling:
                UpdatePatrolling();
                break;

            case EnemyState.Chasing:
                UpdateChasing();
                break;

            case EnemyState.Investigating:
                UpdateInvestigating();
                break;

            case EnemyState.Scanning:
                UpdateScanning();
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
            patrolPointIndex =
                (patrolPointIndex + 1) % patrolPoints.Length;

            SetPatrolTarget(patrolPoints[patrolPointIndex]);
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
        ChangeState(EnemyState.Chasing);
        Debug.Log("Enemy detected player, chasing");
        lastKnownPlayerPosition = player.position;
        agent.SetDestination(lastKnownPlayerPosition);
    }

    private void EnterPatrollingState()
    {
        ChangeState(EnemyState.Patrolling);
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
        ChangeState(EnemyState.Investigating);

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

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            EnterScanningState();
        }
    }

    private void EnterScanningState()
    {
        ChangeState(EnemyState.Scanning);
        investigationTimer = 0f;

        Debug.Log("Enemy started scanning");
    }

    private void UpdateScanning()
    {
        if (playerDetection.IsPlayerDetected)
        {
            EnterChasingState();
            return;
        }

        transform.Rotate(
            Vector3.up,
            investigationRotationSpeed * Time.deltaTime
        );

        investigationTimer += Time.deltaTime;

        if (investigationTimer >= investigationDuration)
        {
            EnterPatrollingState();
        }
    }

    private void ChangeState(EnemyState newState)
    {
        CurrentState = newState;
        StateChanged?.Invoke(CurrentState);
    }
}
