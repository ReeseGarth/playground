using System;
using UnityEngine;

public class CleanableMess : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string displayName = "spill";

    private bool isCleaned;

    public event Action<CleanableMess> Cleaned;

    public string GetInteractionPrompt(
        GameObject interactor
    )
    {
        return $"clean {displayName}";
    }

    public void Interact(GameObject interactor)
    {
        if (isCleaned)
        {
            return;
        }

        isCleaned = true;

        Cleaned?.Invoke(this);

        Debug.Log($"Cleaned {displayName}", this);

        Destroy(gameObject);
    }
}
