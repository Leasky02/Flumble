using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    private int ticks;
    private bool timerActive = false;
    public AudioClip audioClip;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!timerActive && GetComponent<Renderer>().isVisible)
        {
            GetComponent<AudioSource>().clip = audioClip;
            GetComponent<AudioSource>().Play();
            timerActive = true;
        }

    }

    private void FixedUpdate()
    {
        if(timerActive)
        {
            ticks++;
            if (ticks > 15)
            {
                timerActive = false;
                ticks = 0;
            }
        }
    }
}
