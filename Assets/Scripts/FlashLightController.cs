using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightController : MonoBehaviour
{
    public GameObject flashlight;
    private bool isWallToggleLayer = false;

    void Update()
    {
        // Check if the flashlight is toggled
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlight.SetActive(!flashlight.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isWallToggleLayer)
            {
                gameObject.layer = LayerMask.NameToLayer("WallToggle"); // Change layer to WallToggle
            }
            else
            {
                gameObject.layer = LayerMask.NameToLayer("Default"); // Revert to the default layer
            }

            isWallToggleLayer = !isWallToggleLayer; // Toggle the state
        }
      
    }
}
