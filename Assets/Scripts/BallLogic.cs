using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLogic : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.Play();
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().HitEnemy();
            Destroy(gameObject);
        }
    }
}
