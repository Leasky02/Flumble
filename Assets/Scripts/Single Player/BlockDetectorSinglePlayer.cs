using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDetectorSinglePlayer : MonoBehaviour
{
    private bool detecting = true;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private GameObject gameManager;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("block"))
        {
            if (collision.gameObject.GetComponent<DragAndDrop>().canBeDestroyed == true)
            {
                //Debug.Log(detecting);
                //destroy object
                Destroy(collision.collider);
                if (detecting)
                {
                    //play audio
                    GetComponent<AudioSource>().clip = audioClip;
                    GetComponent<AudioSource>().Play();

                    //call player out
                    gameManager.GetComponent<SinglePlayerStructure>().LoseLife();
                    detecting = false;
                }

                //play particles
                collision.collider.GetComponent<SpriteRenderer>().enabled = false;
                collision.collider.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                collision.collider.GetComponent<ParticleSystem>().Play();
                collision.gameObject.GetComponent<DragAndDrop>().canBeDestroyed = false;
            }
        }
    }

    public void SetDetecting()
    {
        detecting = true;
    }
}
