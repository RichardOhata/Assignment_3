using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLogic : MonoBehaviour
{
    public GameObject gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController"); 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            GameLogic gameLogic = gameManager?.GetComponent<GameLogic>();
            gameLogic?.UpdateScore(other.gameObject,other.gameObject.GetComponent<Rigidbody>().velocity.x);
        }
      
    }
}
