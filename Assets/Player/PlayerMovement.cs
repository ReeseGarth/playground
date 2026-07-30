using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Min(0f)]
    private float movementSpeed = 5f;

    [SerializeField, Min(0f)]
    private float gravity = 20f;

    [SerializeField, Min(0f)]
    private float jumpHeight = 1.5f;

    private float verticalVelocity;

    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        Vector2 input = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
            input.y += 1f;

        if (Keyboard.current.sKey.isPressed)
            input.y -= 1f;

        if (Keyboard.current.dKey.isPressed)
            input.x += 1f;

        if (Keyboard.current.aKey.isPressed)
            input.x -= 1f;

        Vector3 horizontalMovement =
            transform.right * input.x +
            transform.forward * input.y;

        horizontalMovement = horizontalMovement.normalized;

        if (characterController.isGrounded)
        {
            verticalVelocity = -1f;

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                verticalVelocity = Mathf.Sqrt(
                    jumpHeight * 2f * gravity
                );
            }
        }
        else
{
            verticalVelocity -= gravity * Time.deltaTime;
        }

        Vector3 velocity =
            horizontalMovement * movementSpeed;

        velocity.y = verticalVelocity;

        characterController.Move(
            velocity * Time.deltaTime
        );
    }
}