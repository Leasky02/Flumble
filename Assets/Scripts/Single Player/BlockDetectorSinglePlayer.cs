using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDetectorSinglePlayer : MonoBehaviour
{
    private bool detecting = true;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private GameObject gameManager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("block"))
        {
            //destroy object
            Destroy(collision.collider, 0.4f);
            if (detecting)
            {
                //play audio
                GetComponent<AudioSource>().clip = audioClip;
                GetComponent<AudioSource>().Play();

                //call player out
                gameManager.GetComponent<SinglePlayerStructure>().EndGame();
                detecting = false;
            }

            //play particles
            collision.collider.GetComponent<SpriteRenderer>().enabled = false;
            collision.collider.GetComponent<ParticleSystem>().Play();

        }
    }

    public void SetDetecting()
    {
        detecting = true;
    }
}
