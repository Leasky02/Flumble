﻿using System.Collections;
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
    // Start is called before the first frame update
    void Awake()
    {
        heightRecord = PlayerPrefs.GetFloat("heightRecord");
        blocksPlacedRecord = PlayerPrefs.GetInt("blocksPlacedRecord");
    }

    void Start()
    {
        if(displayHighscore)
        {
            heightDisplay.GetComponent<Text>().text = ("" + heightRecord + "m");
            blocksPlacedDisplay.GetComponent<Text>().text = ("" + blocksPlacedRecord);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckHeight(float height)
    {
        if(height > heightRecord)
        {
            heightRecord = height;
            PlayerPrefs.SetFloat("heightRecord", heightRecord);
            //set player pref
        }
    }

    public void CheckBlocks(int blocks)
    {
        if(blocks > blocksPlacedRecord)
        {
            blocksPlacedRecord = blocks;
            PlayerPrefs.SetInt("blocksPlacedRecord", blocksPlacedRecord);
        }
    }
}
