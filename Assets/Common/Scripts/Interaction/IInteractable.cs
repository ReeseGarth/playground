using UnityEngine;

public interface IInteractable
{
    string GetInteractionPrompt(GameObject interactor);

    void Interact(GameObject interactor);
}