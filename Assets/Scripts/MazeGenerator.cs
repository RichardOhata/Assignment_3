using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private MazeCell mazeCellPrefab;

    [SerializeField]
    private int mazeHeight;

    [SerializeField]
    private int mazeWidth;

    //private float cellSpacing = 1f;

    [SerializeField]
    private MazeCell[,] mazeGrid;

    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    public NavMeshSurface surface;

    public List<MazeCell> exitCandidates = new List<MazeCell>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitializeMaze());
    }

    private IEnumerator InitializeMaze()
    {
        mazeGrid = new MazeCell[mazeWidth, mazeHeight];
        for (int widthIndex = 0; widthIndex < mazeWidth; widthIndex++)
        {
            for (int heightIndex = 0; heightIndex < mazeHeight; heightIndex++)
            {
                mazeGrid[widthIndex, heightIndex] = Instantiate(mazeCellPrefab, new Vector3(widthIndex, 0, heightIndex), Quaternion.identity, transform);
            }
        }
        GenerateMaze(null, mazeGrid[0, 0]);
        yield return null;
        GenerateExit();
        surface.BuildNavMesh();
        if (PlayerPrefs.HasKey("PlayerPosX") && PlayerPrefs.HasKey("PlayerPosY") && PlayerPrefs.HasKey("PlayerPosZ") &&
         PlayerPrefs.HasKey("EnemyPosX") && PlayerPrefs.HasKey("EnemyPosY") && PlayerPrefs.HasKey("EnemyPosZ"))
        {
            // Load saved positions
            Vector3 playerPosition = new Vector3(
                PlayerPrefs.GetFloat("PlayerPosX"),
                PlayerPrefs.GetFloat("PlayerPosY"),
                PlayerPrefs.GetFloat("PlayerPosZ")
            );

            Vector3 enemyPosition = new Vector3(
                PlayerPrefs.GetFloat("EnemyPosX"),
                PlayerPrefs.GetFloat("EnemyPosY"),
                PlayerPrefs.GetFloat("EnemyPosZ")
            );

            // Spawn player and enemy at their saved positions
            gameManager.SpawnPlayerAtPosition(playerPosition, PlayerPrefs.GetInt("HP"));
            gameManager.SpawnEnemyAtPosition(enemyPosition);
        }
        else
        {
            // If no saved data, spawn as normal
            gameManager.SpawnPlayer(mazeGrid);
            gameManager.SpawnEnemy(mazeGrid[mazeWidth - 1, mazeHeight - 1]);
        }
    }


    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);
        MazeCell nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);     
    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);

        return unvisitedCells.OrderBy(_ => Random.Range(1,10)).FirstOrDefault(); 
    }

    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = (int)currentCell.transform.position.x;
        int z = (int)currentCell.transform.position.z;

        if (x + 1 < mazeWidth)
        {
            var cellToRight = mazeGrid[x + 1, z];
            if (!cellToRight.IsVisited) yield return cellToRight;
        }

        if (x - 1 >= 0)
        {
            var cellToLeft = mazeGrid[x - 1, z];
            if (!cellToLeft.IsVisited) yield return cellToLeft;
        }

        if (z + 1 < mazeHeight)
        {
            var cellToFront = mazeGrid[x, z + 1];
            if (!cellToFront.IsVisited) yield return cellToFront;
        }

        if (z - 1 >= 0)
        {
            var cellToBack = mazeGrid[x, z - 1];
            if (!cellToBack.IsVisited) yield return cellToBack;
        }
    }

    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null)
        {
            return;
        }

        if (previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.ClearWall(MazeCell.Orientation.East);
            currentCell.ClearWall(MazeCell.Orientation.West);
            return;
        }

        if (previousCell.transform.position.x > currentCell.transform.position.x)
        {
            previousCell.ClearWall(MazeCell.Orientation.West);
            currentCell.ClearWall(MazeCell.Orientation.East);
            return;
        }

        if (previousCell.transform.position.z < currentCell.transform.position.z)
        {
            previousCell.ClearWall(MazeCell.Orientation.North);
            currentCell.ClearWall(MazeCell.Orientation.South);
            return;
        }

        if (previousCell.transform.position.z > currentCell.transform.position.z)
        {
            previousCell.ClearWall(MazeCell.Orientation.South);
            currentCell.ClearWall(MazeCell.Orientation.North);
            return;
        }
    }

    private void GenerateExit()
    {
        int rows = mazeGrid.GetLength(0);
        int columns = mazeGrid.GetLength(1);
        List<MazeCell> exitOptions = new List<MazeCell>();

        for (int widthIndex = 0; widthIndex < rows; widthIndex++)
        {
            for (int heightIndex = 0; heightIndex < columns; heightIndex++)
            {
               if (mazeGrid[widthIndex, heightIndex].GetWallsCleared() == 1 && (widthIndex != 0 || heightIndex != 0))
                {
                    exitOptions.Add(mazeGrid[widthIndex, heightIndex]);
                    exitCandidates.Add(mazeGrid[widthIndex, heightIndex]);
                }
            }
        }
        int randomIndex = Random.Range(0, exitOptions.Count);
        MazeCell firstExit = exitOptions[randomIndex];
        firstExit.EnableExitZone();
        Debug.Log(exitCandidates.Count);
        MazeCell secondExit;
        do
        {
            randomIndex = Random.Range(0, exitOptions.Count);
            secondExit = exitOptions[randomIndex];
        }
        while (secondExit == firstExit); // Ensure it's a different candidate

        // Enable the door on the second exit
        secondExit.EnableDoor();
    }

}
