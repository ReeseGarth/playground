using System;
using UnityEngine;

public class SoundStimulusEmitter : MonoBehaviour
{
    [SerializeField, Min(0f)]
    private float radius = 5f;

    public static event Action<SoundStimulus> StimulusEmitted;

    public void Emit()
    {
        SoundStimulus stimulus =
            new SoundStimulus(transform.position, radius);

        StimulusEmitted?.Invoke(stimulus);

        Debug.Log(
            $"Sound emitted at {stimulus.Position} " +
            $"with radius {stimulus.Radius}"
        );
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(
            transform.position,
            radius
        );
    }
}