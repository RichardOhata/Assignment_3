using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player2Control : MonoBehaviour
{
    private InputAction2 inputActions;
    private InputAction movement;
    public Transform playerCamera;
    Rigidbody rb;
    public float moveSpeed = 5f;

    private void Awake()
    {
        inputActions = new InputAction2();
        rb = GetComponent<Rigidbody>(); // get rigidbody
        movement = inputActions.Player.Movement; // get reference to movement action
    }

    private void OnEnable()
    {
        movement.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
    }

    private void FixedUpdate()
    {
        // Get input as a 2D vector (X and Z for horizontal movement)
        Vector2 inputValue = movement.ReadValue<Vector2>();
        Vector3 inputDirection = new Vector3(inputValue.x, 0f, inputValue.y);  // Convert to 3D space

        // Get the camera's forward and right directions, ignoring the Y axis (vertical movement)
        Vector3 cameraForward = playerCamera.forward;
        Vector3 cameraRight = playerCamera.right;

        cameraForward.y = 0f;  // Flatten the forward direction (ignore Y-axis)
        cameraRight.y = 0f;    // Flatten the right direction (ignore Y-axis)

        cameraForward.Normalize();  // Normalize to avoid uneven movement speed
        cameraRight.Normalize();    // Normalize to avoid uneven movement speed

        // Calculate movement direction relative to the camera orientation
        Vector3 moveDirection = (cameraForward * inputDirection.z + cameraRight * inputDirection.x).normalized;

        // Apply movement to the Rigidbody
        MovePlayer(moveDirection);
    }

    private void MovePlayer(Vector3 moveDirection)
    {
        // Apply the movement to the Rigidbody by setting the velocity directly
        // Maintain the current Y velocity (e.g., for jumping or falling)
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
    }
}
