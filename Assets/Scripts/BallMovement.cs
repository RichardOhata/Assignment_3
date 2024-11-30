using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float speed = 5f; // speed of pong ball
    public Vector3 velocity;
    Rigidbody rb;
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InitialDirection();
    }

    void FixedUpdate()
    {
        velocity = rb.velocity;
        ConstantVelocity();
    }

    // Randomizes initial ball direction (up/down/left/right)
    void InitialDirection()
    {
        float horizontalDir = (Random.Range(0, 2) == 0) ? speed : -speed;
        float verticalDir = (Random.Range(0, 2) == 0) ? speed : -speed;
        rb.velocity = new Vector3(horizontalDir, 0, verticalDir);
    }

    // Ensures the velocity is kept at a constant rate
    void ConstantVelocity()
    {
        Vector3 currentVelocity = rb.velocity;

        // Ensures direction is kept the same
        float newX = (currentVelocity.x >= 0) ? speed : -speed; 
        float newZ = (currentVelocity.z >= 0) ? speed : -speed; 

        rb.velocity = new Vector3(newX, 0, newZ);
    }
}
