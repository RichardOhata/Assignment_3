using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameType : MonoBehaviour
{
    public static int matchType;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void ButtonClicked(string button)
    {
        if (button == "Exit")
        {
            SceneManager.LoadScene("Game");
            return;
        }
        if (button == "PVP")
        {
           matchType = 0;
        } else if (button == "PVC")
        {
           matchType = 1;
        }
        SceneManager.LoadScene("PongGame");
    }
}
