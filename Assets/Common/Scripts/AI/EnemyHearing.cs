using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyHearing : MonoBehaviour
{
    private EnemyMovement enemyMovement;

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    private void OnEnable()
    {
        SoundStimulusEmitter.StimulusEmitted +=
            HandleStimulusEmitted;
    }

    private void OnDisable()
    {
        SoundStimulusEmitter.StimulusEmitted -=
            HandleStimulusEmitted;
    }

    private void HandleStimulusEmitted(
        SoundStimulus stimulus
    )
    {
        float distanceToStimulus = Vector3.Distance(
            transform.position,
            stimulus.Position
        );

        if (distanceToStimulus > stimulus.Radius)
        {
            return;
        }

        enemyMovement.Investigate(stimulus.Position);
    }
}
