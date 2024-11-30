using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject fog;
    private BGMLogic bgmLogic;
    private void Start()
    {
        bgmLogic = GameObject.FindGameObjectWithTag("bgm").GetComponent<BGMLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle fog on key press
        if (Input.GetKeyDown(KeyCode.Y))
        {
            fog.SetActive(!fog.activeSelf);
            bgmLogic.AdjustVolume(fog.activeSelf);
        }
    }
}
