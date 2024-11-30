using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightAmbience : MonoBehaviour
{
    public Material[] mazeMaterials; // Array of materials for walls, floors, etc.
    private bool isDay = false;
    private BGMLogic bgmLogic;

    public GameObject mazeParent;
    private void Start()
    {
           bgmLogic = GameObject.FindGameObjectWithTag("bgm").GetComponent<BGMLogic>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // Toggle with "T"
        {
            isDay = !isDay;
            UpdateWallMaterial(isDay);
            bgmLogic.SwapMusic(isDay);
            // Set the BlendFactor based on the time of day
            float blendFactor = isDay ? 2.0f : 0.0f;

            // Apply the BlendFactor to all materials
            foreach (var material in mazeMaterials)
            {
                material.SetFloat("_BlendFactor", blendFactor);
            }
        }
    }

    public void UpdateWallMaterial(bool cond)
    {
        if (mazeParent == null)
        {
            Debug.LogError("Maze parent is not assigned!");
            return;
        }

        // Iterate over all child objects of the maze
        foreach (Transform mazeCell in mazeParent.transform)
        {
            // For each MazeCell, iterate over its children (walls)
            foreach (Transform wall in mazeCell)
            {
                // Try to get the WallMaterial component
                WallMaterial wallMaterial = wall.GetComponent<WallMaterial>();
                if (wallMaterial != null)
                {
                    // Call SetMaterial based on isDaytime
                    wallMaterial.SetMaterial(cond);
                }
            }
        }
    }
}
