using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(AudioSource))]
public class EnemyStateAudio : MonoBehaviour
{
    [SerializeField]
    private AudioClip alertClip;

    [SerializeField]
    private AudioClip searchingClip;

    private EnemyMovement enemyMovement;
    private AudioSource audioSource;

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        audioSource = GetComponent<AudioSource>();
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
            case EnemyState.Chasing:
                audioSource.PlayOneShot(alertClip);
                break;

            case EnemyState.Scanning:
                audioSource.PlayOneShot(searchingClip);
                break;
        }
    }
}