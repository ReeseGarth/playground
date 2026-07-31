using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorChangingInteractable : MonoBehaviour, IInteractable
{
    private Renderer objectRenderer;

    private void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    public void Interact(GameObject interactor)
    {
        objectRenderer.material.color = Random.ColorHSV();
    }

    public string GetInteractionPrompt(GameObject interactor)
    {
        return "change colour";
    }
}
