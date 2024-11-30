using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicLightSource : MonoBehaviour
{
    public Material reveal;
    public Light light;
    public Transform player; // Reference to the player GameObject
    public Transform flashlight; // Reference to the flashlight GameObject
    public Transform cinemachineCamera;
    public float downwardAngleOffset = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        reveal.SetVector("_LightPosition", light.transform.position);
        reveal.SetVector("_LightDirection", light.transform.forward);
        reveal.SetFloat("_LightAngle", light.spotAngle);
        if (cinemachineCamera != null && flashlight != null)
        {
            // Align the flashlight's forward vector with the camera's forward vector
            flashlight.forward = cinemachineCamera.forward;

            // Optional: Adjust flashlight position relative to the player
            flashlight.position = player.position + new Vector3(0, 0, 0); // Adjust offset as needed
        }

    }
}
