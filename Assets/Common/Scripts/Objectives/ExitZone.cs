using System;
using UnityEngine;

public class ExitZone : MonoBehaviour
{
    public event Action PlayerEntered;

    private bool hasPlayerEntered;

    [SerializeField]
    private ObjectiveTracker objectiveTracker;

    private void OnTriggerEnter(Collider other)
    {
        if (hasPlayerEntered)
        {
            return;
        }

        PlayerInventory inventory =
            other.GetComponentInParent<PlayerInventory>();

        if (inventory == null)
        {
            return;
        }

        hasPlayerEntered = true;

        objectiveTracker.CompleteExitObjective();

        PlayerEntered?.Invoke();
    }
}
