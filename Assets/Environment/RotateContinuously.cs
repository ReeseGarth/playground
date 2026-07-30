using UnityEngine;
using UnityEngine.InputSystem;

public class RotateContinuously : MonoBehaviour
{
    [SerializeField] private bool isRotating = true;
    [SerializeField] private float speed = 90f;

    private void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            isRotating = !isRotating;
        }

        if (isRotating)
        {
            transform.Rotate(0f, speed * Time.deltaTime, 0f);
        }
    }
}