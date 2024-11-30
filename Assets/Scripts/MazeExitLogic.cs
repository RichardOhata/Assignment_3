using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeExitLogic : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        gameManager.ExitGame();
    }
}
