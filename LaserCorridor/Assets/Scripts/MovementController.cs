using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    Vector2 moveInput;
    float gravity = -9.81f;

    CharacterController controller;
    Vector3 playerVelocity;
    PlayerStats playerStats;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        // Keep the player grounded
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        // Horizontal movement
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

        controller.Move(move * playerStats.GetCurrentSpeed() * Time.deltaTime);

        // Gravity
        playerVelocity.y += gravity * Time.deltaTime;

        // Vertical movement
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && controller.isGrounded)
        {
            playerVelocity.y =
                Mathf.Sqrt(playerStats.GetCurrentJumpHeight() * -2f * gravity);
        }
    }

    public void ResetVelocity()
    {
        playerVelocity = Vector3.zero;
    }
}