using UnityEngine;

public class ExitZone : MonoBehaviour
{
    [SerializeField]
    private ObjectiveTracker objectiveTracker;

    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory inventory =
            other.GetComponentInParent<PlayerInventory>();

        if (inventory == null)
        {
            return;
        }

        objectiveTracker.CompleteExitObjective();
    }
}