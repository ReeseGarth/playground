using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField, Min(0f)]
    private float interactionDistance = 3f;

    [SerializeField]
    private TMP_Text interactionPromptText;

    private void Awake()
    {
        interactionPromptText.gameObject.SetActive(false);
    }

    private void Update()
    {
        Ray ray = new Ray(
            transform.position,
            transform.forward
        );

        IInteractable interactable = null;

        if (Physics.Raycast(
            ray,
            out RaycastHit hit,
            interactionDistance,
            Physics.DefaultRaycastLayers,
            QueryTriggerInteraction.Ignore
        ))
        {
            interactable =
                hit.collider.GetComponentInParent<IInteractable>();
        }

        Debug.DrawRay(
            ray.origin,
            ray.direction * interactionDistance,
            interactable != null ? Color.green : Color.red,
            0f,
            false
        );

        GameObject interactor = transform.root.gameObject;

        if (interactable == null)
        {
            interactionPromptText.gameObject.SetActive(false);
        }
        else
        {
            interactionPromptText.text =
                $"Press E to {interactable.GetInteractionPrompt(interactor)}";

            interactionPromptText.gameObject.SetActive(true);
        }

        if (Keyboard.current == null)
        {
            return;
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            interactable?.Interact(interactor);
        }
    }
}
