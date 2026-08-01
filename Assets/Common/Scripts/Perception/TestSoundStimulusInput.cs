using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SoundStimulusEmitter))]
public class TestSoundStimulusInput : MonoBehaviour
{
    [SerializeField]
    private Key testKey = Key.T;

    private SoundStimulusEmitter soundStimulusEmitter;

    private void Awake()
    {
        soundStimulusEmitter =
            GetComponent<SoundStimulusEmitter>();
    }

    private void Update()
    {
        if (
            Keyboard.current != null &&
            Keyboard.current[testKey].wasPressedThisFrame
        )
        {
            soundStimulusEmitter.Emit();
        }
    }
}