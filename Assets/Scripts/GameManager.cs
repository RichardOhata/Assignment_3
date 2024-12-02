using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Vector3 playerSpawnPos;
    [SerializeField] private Vector3 enemySpawnPos;
    [SerializeField] private GameObject winText;
    private GameObject currentPlayer;
    private GameObject currentEnemy;
    private MazeCell[,] currentMazeGrid;

    [SerializeField]
    private TextMeshProUGUI hpCount;
    [SerializeField]
    private int hp = 0;

    [SerializeField]
    private AudioSource deathSfx;

    public MazeGenerator mazeGenerator;

    public void SpawnPlayer(MazeCell[,] mazeGrid = null)
    {
        if (mazeGrid != null) {
            currentMazeGrid = mazeGrid;
            Vector3 cellPosition = mazeGrid[0, 0].transform.position;
            playerSpawnPos = cellPosition;
        }
        currentPlayer = Instantiate(playerPrefab, new Vector3(playerSpawnPos.x, playerSpawnPos.y + 0.1f, playerSpawnPos.z), Quaternion.identity);
    }

    public void SpawnEnemy(MazeCell mazeCell = null)
    {
        if (mazeCell != null)
        {
            // Get the Renderer component of the cell, if available, to calculate the center
            Renderer cellRenderer = mazeCell.GetComponent<Renderer>();
            if (cellRenderer != null)
            {
                enemySpawnPos = cellRenderer.bounds.center;
            }
            else
            {
                // Fallback to using the cell's transform position if Renderer isn't available
                enemySpawnPos = mazeCell.transform.position;
            }
        }

        List<MazeCell> exitCandidates = mazeGenerator.exitCandidates;
        if (exitCandidates.Count > 0)
        {
            MazeCell randomExit = exitCandidates[Random.Range(0, exitCandidates.Count)];
            enemySpawnPos = randomExit.transform.position;
        }

        currentEnemy = Instantiate(enemyPrefab, enemySpawnPos, Quaternion.identity);
    }

    public void ResetGame()
    {
        Destroy(currentPlayer);
        Destroy(currentEnemy);
        SpawnPlayer();
        SpawnEnemy();
        winText.SetActive(false);
        hp = 0;
        UpdateHPCount();
    }

    public void ExitGame()
    {
        winText.SetActive(true);
    }

    public void UnStuckEnemy()
    {
        Debug.Log("This happened");
        //Destroy(currentEnemy);
        //currentMazeGrid[currentMazeGrid.GetLength(0) - 1, currentMazeGrid.GetLength(1) - 1].gameObject.SetActive(false);
        //SpawnEnemy();
        //StartCoroutine(ActivateCellAfterDelay(1.0f));
    }

    private IEnumerator ActivateCellAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        currentMazeGrid[currentMazeGrid.GetLength(0) - 1, currentMazeGrid.GetLength(1) - 1].gameObject.SetActive(true);
    }

    public void HitEnemy()
    {
        hp++;
        if (hp >= 3)
        {
            deathSfx.Play();
            Destroy(currentEnemy);
            hp = 0;
            StartCoroutine(SpawnEnemyWithDelay());
        }
        UpdateHPCount();
    }
    private IEnumerator SpawnEnemyWithDelay()
    {
        yield return new WaitForSeconds(5f); // Wait for 5 seconds
        SpawnEnemy(); // Call SpawnEnemy method
        hp = 0;
        UpdateHPCount();
    }

    private void UpdateHPCount()
    {
        hpCount.text = "Enemy Hit: " + hp.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SavePositions(currentPlayer.transform.position, currentEnemy.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            ResetSaveData();
        }
    }

    public void SavePositions(Vector3 playerPosition, Vector3 enemyPosition)
    {
        if (currentPlayer == null || currentEnemy == null)
        {
            Debug.LogWarning("Cannot save positions: Player or Enemy is null.");
            return;
        }

        PlayerPrefs.SetFloat("PlayerPosX", playerPosition.x);
        PlayerPrefs.SetFloat("PlayerPosY", playerPosition.y);
        PlayerPrefs.SetFloat("PlayerPosZ", playerPosition.z);

        // Save enemy position
        PlayerPrefs.SetFloat("EnemyPosX", enemyPosition.x);
        PlayerPrefs.SetFloat("EnemyPosY", enemyPosition.y);
        PlayerPrefs.SetFloat("EnemyPosZ", enemyPosition.z);

        // Save HP
        PlayerPrefs.SetInt("HP", hp);
        PlayerPrefs.SetInt("MazeSeed",mazeGenerator.mazeSeed);
        PlayerPrefs.Save(); // Commit changes
        Debug.Log("Player and enemy positions saved.");
    }

    public void SpawnPlayerAtPosition(Vector3 position, int hp)
    {
        this.hp = hp;
        UpdateHPCount();
        currentPlayer = Instantiate(playerPrefab, new Vector3(position.x, 0.1f, position.z), Quaternion.identity);
    }

    public void SpawnEnemyAtPosition(Vector3 position)
    {
        currentEnemy = Instantiate(enemyPrefab, position, Quaternion.identity);
    }

    public void ResetSaveData()
    {
        // Clear PlayerPrefs data
        PlayerPrefs.DeleteKey("PlayerPosX");
        PlayerPrefs.DeleteKey("PlayerPosY");
        PlayerPrefs.DeleteKey("PlayerPosZ");
        PlayerPrefs.DeleteKey("EnemyPosX");
        PlayerPrefs.DeleteKey("EnemyPosY");
        PlayerPrefs.DeleteKey("EnemyPosZ");
        PlayerPrefs.DeleteKey("HP");
        PlayerPrefs.DeleteKey("MazeSeed");

        // Commit changes
        PlayerPrefs.Save();
        Debug.Log("All save data has been reset.");
    }
}
