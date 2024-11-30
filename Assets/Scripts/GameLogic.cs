using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public GameObject ball;
    public int player1Score = 0;
    public int player2Score = 0;
    public TMP_Text p1ScoreText;
    public TMP_Text p2ScoreText;
    public GameObject gameOverText;
    public int winCon = 5;

    private void Start()
    {
        SpawnBall();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    public void SpawnBall()
    {
       GameObject newBall = Instantiate(ball, new Vector3(0, 0.6f, 0), Quaternion.identity);
    }
    public void UpdateScore(GameObject ballInstance,float direction)
    {
            Destroy(ballInstance);
        
            if (direction >= 0)
            {
                player1Score++;
                p1ScoreText.text = string.Format("{0:D2}", player1Score);
            }
            else
            {
                player2Score++;
                p2ScoreText.text = string.Format("{0:D2}", player2Score);
            }
            if (!CheckWinCon())
            {
                SpawnBall();
            }     
    }

    bool CheckWinCon()
    {
        if (player1Score >= winCon || player2Score >= winCon)
        {
            gameOverText.SetActive(true);
            string winText = "Game Over\n" + ((player1Score >= winCon) ? "Player 1" : "Player 2") + " Wins!";
            gameOverText.GetComponent<TextMeshProUGUI>().text = winText;
            return true;
        }
        return false;
    }
}
