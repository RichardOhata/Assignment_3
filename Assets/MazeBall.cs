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
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().HitEnemy();
            Destroy(gameObject);
            return;
        }
    }
}
