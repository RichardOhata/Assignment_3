using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBall : MonoBehaviour
{
    public AudioSource audioSource;

    private void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.tag != "Player")
        {
            audioSource.Play();
        }
        if (collision.gameObject.tag == "Enemy")
        {
            PlayAudioBeforeDestroy();
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().HitEnemy();
            Destroy(gameObject);
            return;
        }
    }

    private void PlayAudioBeforeDestroy()
    {
        // Create a new GameObject to play the audio
        GameObject audioPlayer = new GameObject("AudioPlayer");
        AudioSource tempAudioSource = audioPlayer.AddComponent<AudioSource>();

        // Copy audio settings
        tempAudioSource.clip = audioSource.clip;
        tempAudioSource.volume = audioSource.volume;
        tempAudioSource.pitch = audioSource.pitch;
        tempAudioSource.spatialBlend = audioSource.spatialBlend; // For 3D sounds

        // Play and destroy the temporary audio object after the clip finishes
        tempAudioSource.Play();
        Destroy(audioPlayer, audioSource.clip.length);
    }
}
