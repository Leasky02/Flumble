using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonMovement : MonoBehaviour
{
    private Vector2 newPosition;
    private RectTransform rt;
    private Rigidbody2D rb;

    private bool firstTime = true;
    private bool firstTime2 = true;

    private string sceneToLoad;

    [SerializeField] private GameObject multiplayerSetupTitle;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("playedBefore1") == 0)
            firstTime = true;
        if (PlayerPrefs.GetInt("playedBefore1") == 1)
            firstTime = false;


        if (PlayerPrefs.GetInt("playedBefore2") == 0)
            firstTime2 = true;
        if (PlayerPrefs.GetInt("playedBefore2") == 1)
            firstTime2 = false;


        rt = GetComponent<RectTransform>();
        rb = GetComponent<Rigidbody2D>();
        newPosition = new Vector2(rt.position.x, rt.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        rt.position = Vector2.MoveTowards(rt.position, newPosition, 1.5f);
    }

    public void MoveLeft()
    {
        GetComponent<AudioSource>().Play();
        newPosition = new Vector2(rt.position.x-30, rt.position.y);
    }

    public void MoveRight()
    {
        GetComponent<AudioSource>().Play();
        newPosition = new Vector2(rt.position.x + 30, rt.position.y);
    }

    public void MoveUp()
    {
        GetComponent<AudioSource>().Play();
        newPosition = new Vector2(rt.position.x, rt.position.y + 30);
    }

    public void MoveDown()
    {
        GetComponent<AudioSource>().Play();
        newPosition = new Vector2(rt.position.x, rt.position.y - 30);
    }

    public void AttemptPlayGame1(string scene)
    {
        sceneToLoad = scene;
        if (firstTime)
        {
            MoveUp();
        }
        else
        {
            PlayGame();
        }
    }

    public void MultiplayerSetup(string scene)
    {
        sceneToLoad = scene;
        MoveLeft();
        if(sceneToLoad == ("2 Players"))
        {
            multiplayerSetupTitle.GetComponent<Text>().text = ("2 Players");
        }
        if (sceneToLoad == ("3 Players"))
        {
            multiplayerSetupTitle.GetComponent<Text>().text = ("3 Players");
        }
        if (sceneToLoad == ("4 Players"))
        {
            multiplayerSetupTitle.GetComponent<Text>().text = ("4 Players");
        }
    }

    public void AttemptPlayGame2()
    {
        if (firstTime2)
        {
            MoveUp();
        }
        else
        {
            PlayGame2();
        }
    }

    public void PlayGame()
    {
        PlayerPrefs.SetInt("playedBefore1", 1);
        SceneManager.LoadScene(sceneToLoad);
    }
    public void PlayGame2()
    {
        PlayerPrefs.SetInt("playedBefore2", 1);
        SceneManager.LoadScene(sceneToLoad);
    }
}
