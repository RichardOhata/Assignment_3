using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMaterial : MonoBehaviour
{
    public Material dayMaterial;
    public Material nightMaterial;
    public GameObject graphic;
    // Start is called before the first frame update
    void Start()
    {
        SetMaterial(false);
    }

    public void SetMaterial(bool isDaytime)
    {
        if (graphic != null)
        {
            Renderer renderer = graphic.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = isDaytime ? dayMaterial : nightMaterial;
            }
        }
    }
}
