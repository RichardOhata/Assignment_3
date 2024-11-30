using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private AudioSource spawnAudio;
    public float moveSpeed = 1f; // Movement speed
    public float checkDistance = .15f; // Distance to check for walls
    private Vector3 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
      spawnAudio.Play();
      PickRandomDirection();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the enemy in the current direction
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        Debug.DrawRay(transform.position, moveDirection * checkDistance, Color.yellow, 0.1f);
        // Check if there's a wall in the current direction
        if (IsWallAhead())
        {
            PickRandomDirection();
        }
    }
    private bool IsWallAhead()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, moveDirection, out hit, checkDistance))
        {
            // If the raycast hits something, consider it a wall
            if (hit.collider.CompareTag("Wall"))
            {
                return true;
            }
        }
        return false;
    }

    // Pick a random new direction (up, down, left, or right)
    private void PickRandomDirection()
    {
        int randomDirection = Random.Range(0, 4);
        switch (randomDirection)
        {
            case 0:
                moveDirection = Vector3.forward; // Move up
                break;
            case 1:
                moveDirection = Vector3.back; // Move down
                break;
            case 2:
                moveDirection = Vector3.left; // Move left
                break;
            case 3:
                moveDirection = Vector3.right; // Move right
                break;
        }

        Debug.Log("Picked new direction: " + moveDirection);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Wall")
        {
            PickRandomDirection();
        }
    }

}
