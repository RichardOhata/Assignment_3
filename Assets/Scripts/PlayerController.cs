using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Create private internal references
    private PlayerInput playerInput;  // Reference to PlayerInput component
    private InputActions inputActions;
    private InputAction movement;      // Movement action reference
    private Rigidbody rb;              // Rigidbody reference

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Get Rigidbody
      
        // vs AI
        if (GameType.matchType == 1)
        {
            inputActions = new InputActions();
            movement = inputActions.Player.Movement;
        } else
        {
            // vs Player
            playerInput = GetComponent<PlayerInput>(); // Get PlayerInput component
                                                       // Set up the movement action
            movement = playerInput.actions["Movement"]; // Get reference to the movement action
        }      
    }

    private void OnEnable()
    {
        movement.Enable(); // Enable the movement action
    }

    private void OnDisable()
    {
        movement.Disable(); // Disable the movement action
    }

    private void FixedUpdate()
    {
        Vector2 v2 = movement.ReadValue<Vector2>(); // Read 2D input
        Vector3 v3 = new Vector3(v2.x, 0, v2.y); // Convert to 3D space

        // Apply movement force based on input
        rb.AddForce(v3, ForceMode.VelocityChange);
    }
}