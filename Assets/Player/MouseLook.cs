using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [SerializeField, Min(0f)]
    private float sensitivity = 120f;

    [SerializeField]
    private Transform player;

    private float verticalRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Mouse.current == null)
        {
            return;
        }

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float mouseX = mouseDelta.x * sensitivity * Time.deltaTime;
        float mouseY = mouseDelta.y * sensitivity * Time.deltaTime;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(
            verticalRotation,
            0f,
            0f
        );

        player.Rotate(Vector3.up * mouseX);
    }
}