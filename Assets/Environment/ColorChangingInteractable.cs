using UnityEngine;

public class ColorChangingInteractable : MonoBehaviour, IInteractable
{
    private Renderer objectRenderer;

    private void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    public void Interact()
    {
        objectRenderer.material.color = Random.ColorHSV();
    }
}