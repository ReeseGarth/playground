using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    [SerializeField]
    private float openAngle = 90f;

    [SerializeField, Min(0f)]
    private float rotationSpeed = 180f;

    [SerializeField]
    private string requiredItemId;

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private Quaternion targetRotation;
    private bool isOpen;

    private void Awake()
    {
        closedRotation = transform.localRotation;

        openRotation =
            closedRotation *
            Quaternion.Euler(0f, openAngle, 0f);

        targetRotation = closedRotation;
    }

    private void Update()
    {
        transform.localRotation = Quaternion.RotateTowards(
            transform.localRotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    public void Interact(GameObject interactor)
    {
        if (!isOpen && !CanOpen(interactor))
        {
            Debug.Log(
                $"Door requires {requiredItemId}"
            );

            return;
        }

        isOpen = !isOpen;

        targetRotation =
            isOpen ? openRotation : closedRotation;
    }

    private bool CanOpen(GameObject interactor)
    {
        if (string.IsNullOrEmpty(requiredItemId))
        {
            return true;
        }

        PlayerInventory inventory =
            interactor.GetComponent<PlayerInventory>();

        return inventory != null &&
            inventory.Contains(requiredItemId);
    }

    public string GetInteractionPrompt(GameObject interactor)
    {
        if (isOpen)
        {
            return "close door";
        }

        if (!CanOpen(interactor))
        {
            return $"open door (requires {requiredItemId})";
        }

        return "open door";
    }
}
