using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILogic : MonoBehaviour
{
    public GameObject ball;
    public float speed = 4f;
    public float movementBoundary = 4.5f;
    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<BallMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        TrackBall();
    }

    void TrackBall()
    {
        if (ball == null)
        {
            ball = FindObjectOfType<BallMovement>()?.gameObject;
            return;
        }
        float ballPos = ball.transform.position.z;

        if (transform.position.z > ballPos)
        {
            transform.position += new Vector3(0, 0, -speed * Time.deltaTime);
        }
        if (transform.position.z < ballPos)
        {
            transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        }

        float clampedZ = Mathf.Clamp(transform.position.z, -movementBoundary, movementBoundary);
        transform.position = new Vector3(transform.position.x, transform.position.y, clampedZ);
    }
}
