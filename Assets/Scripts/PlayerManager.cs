using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerManager : MonoBehaviour
{
    public GameObject playerPrefab;
    Vector3 player1Position = new Vector3(-8.5f, 0.6f, 0.34f);
    Vector3 player2Position = new Vector3(8.5f, 0.6f, 0.34f);

    // Start is called before the first frame update
    void Start()
    {
        spawnPlayers();
    }

    void spawnPlayers()
    {

        //Join Player 1(Keyboard)
        PlayerInput player1 = PlayerInputManager.instance.JoinPlayer(0, -1, "Keyboard");

        //// Set the position after player is created
        player1.gameObject.transform.position = player1Position;

        // Join Player 2 (Gamepad)
        PlayerInput player2 = PlayerInputManager.instance.JoinPlayer(1, 1, "Gamepad");
        
        ////// Set the position after player is created
        player2.gameObject.transform.position = player2Position;

        if (GameType.matchType == 1)
        {
            player1.neverAutoSwitchControlSchemes = false;
            Destroy(player2.GetComponent<PlayerController>());
            Destroy(player2);
            player2.AddComponent<AILogic>();
        }
    }

}
