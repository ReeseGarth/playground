using UnityEngine;

public readonly struct SoundStimulus
{
    public Vector3 Position { get; }
    public float Radius { get; }

    public SoundStimulus(Vector3 position, float radius)
    {
        Position = position;
        Radius = radius;
    }
}