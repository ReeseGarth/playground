using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyStateIndicator : MonoBehaviour
{
    [SerializeField]
    private Renderer indicatorRenderer;

    [SerializeField]
    private Color patrollingColor = Color.green;

    [SerializeField]
    private Color chasingColor = Color.red;

    [SerializeField]
    private Color investigatingColor = Color.yellow;

    private EnemyMovement enemyMovement;

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    private void OnEnable()
    {
        enemyMovement.StateChanged += HandleStateChanged;
    }

    private void OnDisable()
    {
        enemyMovement.StateChanged -= HandleStateChanged;
    }

    private void HandleStateChanged(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Patrolling:
                indicatorRenderer.material.color = patrollingColor;
                break;

            case EnemyState.Chasing:
                indicatorRenderer.material.color = chasingColor;
                break;

            case EnemyState.Investigating:
            case EnemyState.Scanning:
                indicatorRenderer.material.color = investigatingColor;
                break;
        }
    }
}
