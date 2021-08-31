using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject finishButton;
    [SerializeField] private GameObject toggleVisibility;

    private string currentScene;

    private Vector2 buttonPosition;
    private Vector2 pauseScreenPosition;
    void Awake()
    {
        
        buttonPosition = new Vector2(finishButton.GetComponent<RectTransform>().position.x, finishButton.GetComponent<RectTransform>().position.y);
        pauseScreenPosition = new Vector2(pauseScreen.GetComponent<RectTransform>().position.x, pauseScreen.GetComponent<RectTransform>().position.y);
    }

    private void Start()
    {
        pauseScreen.GetComponent<RectTransform>().position = new Vector2(pauseScreenPosition.x + 100, pauseScreenPosition.y);
    }

    public void PauseScreen()
    {
        finishButton.GetComponent<RectTransform>().position = new Vector2(buttonPosition.x + 100, finishButton.GetComponent<RectTransform>().position.y);
        pauseScreen.GetComponent<RectTransform>().position = new Vector2(pauseScreenPosition.x , pauseScreen.GetComponent<RectTransform>().position.y);

        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == ("Free Play"))
        {
            toggleVisibility.GetComponent<Image>().enabled = false;
            toggleVisibility.GetComponent<Button>().enabled = false;
        }
    }

    public void UnpauseScreen()
    {
        finishButton.GetComponent<RectTransform>().position = new Vector2(buttonPosition.x, finishButton.GetComponent<RectTransform>().position.y);
        pauseScreen.GetComponent<RectTransform>().position = new Vector2(pauseScreenPosition.x + 100, pauseScreen.GetComponent<RectTransform>().position.y);

        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == ("Free Play"))
        {
            toggleVisibility.GetComponent<Image>().enabled = true;
            toggleVisibility.GetComponent<Button>().enabled = true;
        }
    }
}
