using UnityEngine;

[RequireComponent(typeof(PlayerDetection))]
public class EnemyCapture : MonoBehaviour
{
    [SerializeField]
    private PlayerCapture player;

    [SerializeField, Min(0f)]
    private float captureDistance = 1f;

    private PlayerDetection playerDetection;

    private void Awake()
    {
        playerDetection =
            GetComponent<PlayerDetection>();
    }

    private void Update()
    {
        if (player.IsCaptured)
        {
            return;
        }

        if (!playerDetection.IsPlayerDetected)
        {
            return;
        }

        Vector3 offsetToPlayer =
            player.transform.position - transform.position;

        offsetToPlayer.y = 0f;

        float distanceToPlayer =
            offsetToPlayer.magnitude;

        if (distanceToPlayer <= captureDistance)
        {
            player.Capture();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            transform.position,
            captureDistance
        );
    }
}