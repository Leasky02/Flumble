using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    private static float heightRecord = 0;
    private static int blocksPlacedRecord = 0;

    [SerializeField] private GameObject heightDisplay;
    [SerializeField] private GameObject blocksPlacedDisplay;

    [SerializeField] private bool displayHighscore;

    [SerializeField] private GameObject highScoreText;
    [SerializeField] private GameObject blocksPlacedScore;
    [SerializeField] private GameObject heightScore;

    //particles
    [SerializeField] private GameObject[] particles;
    // Start is called before the first frame update
    void Awake()
    {
        heightRecord = PlayerPrefs.GetFloat("heightRecord1");
        blocksPlacedRecord = PlayerPrefs.GetInt("blocksPlacedRecord");

        //Debug.Log(heightRecord);
    }

    void Start()
    {
        if(displayHighscore)
        {
            heightDisplay.GetComponent<Text>().text = ("" + heightRecord + "m");
            blocksPlacedDisplay.GetComponent<Text>().text = ("" + blocksPlacedRecord);
        }
    }

    public void CheckHeight(float height)
    {
        if(height > heightRecord)
        {
            heightRecord = height;
            PlayerPrefs.SetFloat("heightRecord1", heightRecord);
            highScoreText.GetComponent<Text>().enabled = true;
            heightScore.GetComponent<Text>().color = new Color(130f/255f , 231f/255f , 130f/255f, 1f);

            particles[0].GetComponent<ParticleSystem>().Play();
            particles[1].GetComponent<ParticleSystem>().Play();
            particles[2].GetComponent<ParticleSystem>().Play();
            GetComponent<AudioSource>().Play();
        }
        else
        {
            PlayerPrefs.SetFloat("heightRecord1", heightRecord);
        }
    }

    public void CheckBlocks(int blocks)
    {
        if(blocks > blocksPlacedRecord)
        {
            blocksPlacedRecord = blocks;
            PlayerPrefs.SetInt("blocksPlacedRecord", blocksPlacedRecord);
            highScoreText.GetComponent<Text>().enabled = true;
            blocksPlacedScore.GetComponent<Text>().color = new Color(130f / 255f, 231f / 255f, 130f / 255f, 1f);

            particles[0].GetComponent<ParticleSystem>().Play();
            particles[1].GetComponent<ParticleSystem>().Play();
            particles[2].GetComponent<ParticleSystem>().Play();
            GetComponent<AudioSource>().Play();
        }
    }
}
