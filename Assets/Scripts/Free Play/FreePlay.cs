using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreePlay : MonoBehaviour
{
    [SerializeField] GameObject startScreen;
    [SerializeField] GameObject exitButton;
    [SerializeField] GameObject colourButton;
    [SerializeField] GameObject greyPlatform;

    [SerializeField] GameObject toolbox;
    private Vector2 toolboxLocation;

    [SerializeField] GameObject toggleViewButton;

    private bool visible = true;
    [SerializeField] Slider slider;

    [SerializeField] private GameObject[,] blocks = new GameObject[4, 4];
    [SerializeField] private GameObject[] squares;
    [SerializeField] private GameObject[] triangles;
    [SerializeField] private GameObject[] circles;
    [SerializeField] private GameObject[] lines;
    private Vector2 exitButtonLocation;
    private Vector2 toggleViewLocation;

    //colours
    private int colourValue = 0;
    private Color[] colours = new Color[4];

    private void Start()
    {

        //set UI element position
        exitButtonLocation = exitButton.GetComponent<RectTransform>().position;
        exitButton.GetComponent<Transform>().position = new Vector2(exitButtonLocation.x + 100, exitButtonLocation.y);

        toggleViewLocation = toggleViewButton.GetComponent<RectTransform>().position;
        toggleViewButton.GetComponent<Transform>().position = new Vector2(toggleViewLocation.x + 100, toggleViewLocation.y);

        toolboxLocation = toolbox.GetComponent<RectTransform>().position;
        toolbox.GetComponent<RectTransform>().position = new Vector2(toolboxLocation.x + 100, toolbox.GetComponent<RectTransform>().position.y);

        //set shapes
        blocks[0, 0] = squares[0];
        blocks[0, 1] = squares[1];
        blocks[0, 2] = squares[2];
        blocks[0, 3] = squares[3];

        blocks[1, 0] = triangles[0];
        blocks[1, 1] = triangles[1];
        blocks[1, 2] = triangles[2];
        blocks[1, 3] = triangles[3];

        blocks[2, 0] = lines[0];
        blocks[2, 1] = lines[1];
        blocks[2, 2] = lines[2];
        blocks[2, 3] = lines[3];

        blocks[3, 0] = circles[0];
        blocks[3, 1] = circles[1];
        blocks[3, 2] = circles[2];
        blocks[3, 3] = circles[3];

        //set colours
        colours[0] = new Color(222f / 255f, 233f / 255f, 48f / 255f); //yellow
        colours[1] = new Color(65f / 255f, 48f / 255f, 233f / 255f); //blue
        colours[2] = new Color(65f / 255f, 233f / 255f, 48f / 255f); //green
        colours[3] = new Color(233f / 255f, 48f / 255f, 48f / 255f); //red

        colourButton.GetComponent<Image>().color = colours[0];
    }
    public void Begin()
    {
        //sorting UI elements into position
        startScreen.GetComponent<Transform>().position = new Vector2(100f, 0f);
        toggleViewButton.GetComponent<Transform>().position = new Vector2(toggleViewLocation.x, toggleViewLocation.y);
        exitButton.GetComponent<Transform>().position = new Vector2(exitButtonLocation.x, exitButtonLocation.y);
        toolbox.GetComponent<RectTransform>().position = new Vector2(toolboxLocation.x, toolbox.GetComponent<RectTransform>().position.y);
    }

    public void SpawnShape(int shape)
    {
        Instantiate(blocks[shape, colourValue], new Vector2 (greyPlatform.GetComponent<Transform>().position.x, greyPlatform.GetComponent<Transform>().position.y), Quaternion.identity);
    }

    public void SetColour()
    {
        colourValue++;
        if (colourValue > 3)
            colourValue = 0;

        switch(colourValue)
        {
            case 0:
                colourButton.GetComponent<Image>().color = colours[0];
                break;
            case 1:
                colourButton.GetComponent<Image>().color = colours[1];
                break;
            case 2:
                colourButton.GetComponent<Image>().color = colours[2];
                break;
            case 3:
                colourButton.GetComponent<Image>().color = colours[3];
                break;
        }
    }

    public void ChangeSize()
    {
        GetComponent<DragAndDrop>().SetSize(slider.GetComponent<Slider>().value);
    }

    public void ToggleView()
    {
        if(visible)
        {
            visible = false;
            toggleViewButton.GetComponent<SpriteRenderer>().enabled = true;
            toolbox.GetComponent<RectTransform>().position = new Vector2(toolboxLocation.x + 100, toolbox.GetComponent<RectTransform>().position.y);
        }
        else if(!visible)
        {
            visible = true;
            toggleViewButton.GetComponent<SpriteRenderer>().enabled = false;
            toolbox.GetComponent<RectTransform>().position = new Vector2(toolboxLocation.x, toolbox.GetComponent<RectTransform>().position.y);
        }
    }

}
