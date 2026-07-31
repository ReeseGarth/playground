using UnityEngine;

public class PickupInteractable : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string itemId = "key";

    public void Interact(GameObject interactor)
    {
        PlayerInventory inventory =
            interactor.GetComponent<PlayerInventory>();

        if (inventory == null)
        {
            Debug.LogWarning(
                $"{interactor.name} does not have a PlayerInventory"
            );

            return;
        }

        if (inventory.Add(itemId))
        {
            Destroy(gameObject);
        }
    }

    public string GetInteractionPrompt(GameObject interactor)
    {
        return $"pick up {itemId}";
    }
}
