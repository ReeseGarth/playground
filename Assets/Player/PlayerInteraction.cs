using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField, Min(0f)]
    private float interactionDistance = 3f;

    private void Update()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        if (!Keyboard.current.eKey.wasPressedThisFrame)
        {
            return;
        }

        Debug.DrawRay(
            transform.position,
            transform.forward * interactionDistance,
            Color.red
        );

        Ray ray = new Ray(transform.position, transform.forward);

        if (!Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
        {
            return;
        }

        IInteractable interactable =
            hit.collider.GetComponent<IInteractable>();

        interactable?.Interact();
    }
}