using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    public enum Orientation
    {
        North,
        East,
        West,
        South
    }

    [SerializeField]
    private GameObject northWall;

    [SerializeField] 
    private GameObject eastWall;

    [SerializeField]
    private GameObject westWall;

    [SerializeField]
    private GameObject southWall;

    [SerializeField]
    private GameObject exitZone;

    [SerializeField]
    private GameObject doorPrefab;

    [SerializeField]
    private int wallsCleared = 0;
    public bool IsVisited { get; private set; }

    public int GetWallsCleared()
    {
        return wallsCleared;
    }

    public void Visit()
    {
        IsVisited = true;
    }

    public void EnableExitZone()
    {
        exitZone.SetActive(true);
    }

    public void EnableDoor()
    {
        // List of walls to check in order
        GameObject[] walls = { northWall, eastWall, westWall, southWall };

        foreach (GameObject wall in walls)
        {
            if (wall.activeSelf) // Check if the wall is active
            {
                // Instantiate the door prefab at the wall's position and rotation
                Instantiate(doorPrefab, wall.transform.position, wall.transform.rotation);

                // Optionally deactivate the wall after replacing it with a door
                wall.SetActive(false);
                break; // Exit the loop after the first door is created
            }
        }
    }

    public void ClearWall(Orientation orientation)
    {
        wallsCleared++;
        switch (orientation)
        {
            case Orientation.North:
                northWall.SetActive(false);
                break;
            case Orientation.East:
                eastWall.SetActive(false);
                break;
            case Orientation.West:
                westWall.SetActive(false);
                break;
            case Orientation.South:
                southWall.SetActive(false);
                break;
            default:
                break;
        }
    }
}