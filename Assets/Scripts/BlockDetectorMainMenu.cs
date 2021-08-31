using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDetectorMainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("block"))
        {
            //detroy object
            Destroy(collision.collider, 0.4f);
            //play audio
            GetComponent<AudioSource>().clip = audioClip;
            GetComponent<AudioSource>().Play();

            //play particles
            collision.collider.GetComponent<SpriteRenderer>().enabled = false;
            collision.collider.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            collision.collider.GetComponent<ParticleSystem>().Play();
        }
    }
}
