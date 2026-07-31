using System;
using UnityEngine;

public class PlayerCapture : MonoBehaviour
{
    public event Action Captured;

    public bool IsCaptured { get; private set; }

    public void Capture()
    {
        if (IsCaptured)
        {
            return;
        }

        IsCaptured = true;

        Debug.Log("Player captured");

        Captured?.Invoke();
    }
}