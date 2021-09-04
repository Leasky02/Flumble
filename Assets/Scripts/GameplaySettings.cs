using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplaySettings : MonoBehaviour
{
    private static int lives = 3;
    private static int baseWidth = 3;

    [SerializeField] private bool setupScene;

    [SerializeField] private GameObject lives_txt;
    [SerializeField] private GameObject base_txt;

    [SerializeField] private GameObject[] livesValueChangers;
    [SerializeField] private GameObject[] baseValueChangers;

    [SerializeField] private GameObject[] baseBlocks;
    // Start is called before the first frame update
    void Start()
    {
        if(setupScene)
        {
            lives_txt.GetComponent<Text>().text = ("" + lives);
            base_txt.GetComponent<Text>().text = ("" + baseWidth);
        }
        else
        {
            Debug.Log("setting base");
            if(baseWidth <= 4)
            {
                baseBlocks[5].GetComponent<SpriteRenderer>().enabled = false;
                baseBlocks[5].GetComponent<BoxCollider2D>().enabled = false;
            }
            if(baseWidth <= 3)
            {
                baseBlocks[4].GetComponent<SpriteRenderer>().enabled = false;
                baseBlocks[4].GetComponent<BoxCollider2D>().enabled = false;
            }
            if (baseWidth <= 2)
            {
                baseBlocks[3].GetComponent<SpriteRenderer>().enabled = false;
                baseBlocks[3].GetComponent<BoxCollider2D>().enabled = false;
            }
            if (baseWidth <= 1)
            {
                baseBlocks[2].GetComponent<SpriteRenderer>().enabled = false;
                baseBlocks[2].GetComponent<BoxCollider2D>().enabled = false;
            }
            if (baseWidth <= 0)
            {
                baseBlocks[1].GetComponent<SpriteRenderer>().enabled = false;
                baseBlocks[1].GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    public void AddLives()
    {
        lives++;
        if (lives > 4)
        {
            lives = 5;
            livesValueChangers[1].GetComponent<Button>().interactable = false;
        }
        livesValueChangers[0].GetComponent<Button>().interactable = true;
        lives_txt.GetComponent<Text>().text = ("" + lives);
    }

    public void SubtractLives()
    {
        lives--;
        if (lives < 2)
        {
            lives = 1;
            livesValueChangers[0].GetComponent<Button>().interactable = false;
        }
        livesValueChangers[1].GetComponent<Button>().interactable = true;
        lives_txt.GetComponent<Text>().text = ("" + lives);
    }

    public void AddBaseWidth()
    {
        baseWidth++;
        if (baseWidth > 4)
        {
            baseWidth = 5;
            baseValueChangers[1].GetComponent<Button>().interactable = false;
        }
        baseValueChangers[0].GetComponent<Button>().interactable = true;
        base_txt.GetComponent<Text>().text = ("" + baseWidth);
    }

    public void SubtractBaseWidth()
    {
        baseWidth--;
        if (baseWidth < 2)
        {
            baseWidth = 1;
            baseValueChangers[0].GetComponent<Button>().interactable = false;
        }
        baseValueChangers[1].GetComponent<Button>().interactable = true;
        base_txt.GetComponent<Text>().text = ("" + baseWidth);
    }

    public int ReturnLives()
    {
        return lives;
    }

}
