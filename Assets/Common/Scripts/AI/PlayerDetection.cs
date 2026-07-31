using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField, Min(0f)]
    private float detectionDistance = 5f;

    [SerializeField]
    private float eyeHeight = 1.5f;

    [SerializeField]
    private float playerTargetHeight = 1f;

    [SerializeField, Range(0f, 360f)]
    private float viewAngle = 120f;

    [SerializeField, Range(0f, 360f)]
    private float trackingViewAngle = 240f;

    public bool IsPlayerDetected { get; private set; }

    private void Update()
    {
        bool wasPlayerDetected = IsPlayerDetected;

        float activeViewAngle =
            wasPlayerDetected
                ? trackingViewAngle
                : viewAngle;

        IsPlayerDetected =
            IsPlayerWithinRange() &&
            IsPlayerWithinViewAngle(activeViewAngle) &&
            HasLineOfSight();

        if (IsPlayerDetected && !wasPlayerDetected)
        {
            Debug.Log("Player detected");
        }
        else if (!IsPlayerDetected && wasPlayerDetected)
        {
            Debug.Log("Player lost");
        }
    }

    private bool IsPlayerWithinRange()
    {
        Vector3 offsetToPlayer =
            player.position - transform.position;

        offsetToPlayer.y = 0f;

        return offsetToPlayer.magnitude <= detectionDistance;
    }

    private bool IsPlayerWithinViewAngle(float angle)
    {
        Vector3 directionToPlayer =
            player.position - transform.position;

        directionToPlayer.y = 0f;

        if (directionToPlayer == Vector3.zero)
        {
            return true;
        }

        float angleToPlayer = Vector3.Angle(
            transform.forward,
            directionToPlayer
        );

        return angleToPlayer <= angle / 2f;
    }

    private bool HasLineOfSight()
    {
        Vector3 origin =
            transform.position +
            Vector3.up * eyeHeight;

        Vector3 destination =
            player.position +
            Vector3.up * playerTargetHeight;

        Vector3 direction =
            destination - origin;

        bool hitSomething = Physics.Raycast(
            origin,
            direction.normalized,
            out RaycastHit hit,
            direction.magnitude,
            Physics.DefaultRaycastLayers,
            QueryTriggerInteraction.Ignore
        );

        bool hitPlayer =
            hitSomething &&
            (
                hit.transform == player ||
                hit.transform.IsChildOf(player)
            );

        Debug.DrawRay(
            origin,
            direction,
            hitPlayer ? Color.green : Color.red,
            0f,
            false
        );

        return hitPlayer;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color =
            IsPlayerDetected ? Color.red : Color.yellow;

        Gizmos.DrawWireSphere(
            transform.position,
            detectionDistance
        );

        DrawViewBoundary(-viewAngle / 2f);
        DrawViewBoundary(viewAngle / 2f);
    }

    private void DrawViewBoundary(float angle)
    {
        Vector3 direction =
            Quaternion.AngleAxis(angle, Vector3.up) *
            transform.forward;

        Gizmos.DrawRay(
            transform.position,
            direction * detectionDistance
        );
    }
}
