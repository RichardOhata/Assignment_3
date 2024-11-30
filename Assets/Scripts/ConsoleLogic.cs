using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConsoleLogic : MonoBehaviour
{
    public GameObject console;
    public TMP_InputField inputField;
    public GameObject gameManager;
    public GameObject background;
    public GameObject consoleLog;

    // Update is called once per frame
    void Update()
    {
        OpenConsole();
        ConsoleInput();
    }

    void OpenConsole()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            console.SetActive(true);
        }
    }

    void ConsoleInput()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string input = inputField.text.Trim();
            consoleLog.GetComponent<TextMeshProUGUI>().text += "\n>" + input;
            inputField.text = "";
            switch (input)
            {
                case "exit":
                case "close":
                    consoleLog.GetComponent<TextMeshProUGUI>().text = "";
                    console.SetActive(false);
                    break;
                case "spawnball":
                    gameManager.GetComponent<GameLogic>().SpawnBall(); 
                    break;
                default:
                    if (input.StartsWith("background "))
                    {
                        string hex = input.Substring("background ".Length);
                        Color newColor;
                        if (ColorUtility.TryParseHtmlString(hex, out newColor))
                        {
                            background.GetComponent<Renderer>().material.color = newColor;
                        }
                        }
                    break;
            };
        }
    }
}
