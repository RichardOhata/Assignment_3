using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    public GameObject ballPrefab; // Reference to the ball prefab
    public Transform throwPoint; // Position to spawn the ball
    public float throwForce = 10f; // Force applied to the ball
    public Camera throwCamera;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse click
        {
            Throw();
        }
    }

    void Throw()
    {
        // Create the ball at the throw point
        GameObject ball = Instantiate(ballPrefab, throwPoint.position, throwPoint.rotation);

        Vector3 direction = throwCamera.transform.forward;

        // Apply force to the ball's Rigidbody
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.AddForce(direction * throwForce, ForceMode.Impulse);

        // Optionally destroy the ball after some time
        Destroy(ball, 3f);
    }
}
