using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMLogic : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;  // Reference to the single AudioSource
    [SerializeField] private AudioClip dayMusicClip;   // Daytime music clip
    [SerializeField] private AudioClip nightMusicClip;
    private Transform enemyTransform;
    private bool isMusicPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.Play();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ControlMusic();
        }
        enemyTransform = GameObject.FindGameObjectWithTag("Enemy")?.transform;
        if (enemyTransform != null)
        {
            transform.position = enemyTransform.position;
        }
    }

    public void ControlMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        } else
        {
            audioSource.Play();
        }
    }

    public void SwapMusic(bool cond)
    {
        if (cond)
        {
            audioSource.clip = dayMusicClip;
        }
        else
        {
            audioSource.clip = nightMusicClip;
          
        }
        audioSource.Play();
    }

    public void AdjustVolume(bool cond)
    {
        if (cond)
        {
            audioSource.volume = 0.5f;
        } else
        {
            audioSource.volume = 1f;
        }
    }
}
