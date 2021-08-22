using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlayerStructure : MonoBehaviour
{
    public GameObject ground;

    //spawn platform for blocks
    [SerializeField] private GameObject platform;
    //start screen
    [SerializeField] private GameObject startScreen;
    //end screen
    [SerializeField] private GameObject EndScreen;
    [SerializeField] private GameObject heightReached;
    [SerializeField] private GameObject blocksPlaced;

    //end screen positioin
    private Vector2 endScreenPosition;
    private Vector2 finishButtonPosition;

    //all shapes in arrays
    [SerializeField] private GameObject[,] blocks = new GameObject[4, 4];
    [SerializeField] private GameObject[] squares;
    [SerializeField] private GameObject[] lines;
    [SerializeField] private GameObject[] triangles;
    [SerializeField] private GameObject[] circles;

    //contains what shapes to use
    private int shapeValue = 0;
    private int colourValue = 0;

    //scores
    private int blockTotal = -1;
    private float maxHeight;

    //UI elements and positions
    [SerializeField] private GameObject finishButton;

    //testing if there is still movement
    private bool stillMovement = false;
    private GameObject[] blocksVelocity;

    public void Start()
    {
        shapeValue = Random.Range(0, 4);

        //set all block values;

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

        //set UI element positions
        endScreenPosition = EndScreen.GetComponent<RectTransform>().position;
        EndScreen.GetComponent<RectTransform>().position = new Vector2(endScreenPosition.x + 100, endScreenPosition.y);

        finishButtonPosition = finishButton.GetComponent<RectTransform>().position;
        finishButton.GetComponent<RectTransform>().position = new Vector2(finishButtonPosition.x + 100, finishButtonPosition.y);
    }

    private void Update()
    {
        //test for all blocks if they are moving
        blocksVelocity = GameObject.FindGameObjectsWithTag("block");

        //tests all blocks if they are moving
        for (int i = 0; i < blocksVelocity.Length; i++)
        {
            if (blocksVelocity[i].GetComponent<Rigidbody2D>().velocity.magnitude > 0f)
            {
                stillMovement = true;
                i = blocksVelocity.Length;
            }
            else
            {
                stillMovement = false;
            }
        }
        //if block is moving or being dragged or hasnt even been moved yet, dont allow button to be pressed
        if (stillMovement || GetComponent<DragAndDrop>().IsItDragging() == true || GetComponent<DragAndDrop>().IsItMoved() == false)
        {
            finishButton.GetComponent<Button>().interactable = false;
        }
        //if there is no movement, let it finish turn
        else if (!stillMovement)
        {
            finishButton.GetComponent<Button>().interactable = true;
        }
    }

    public void Begin()
    {
        finishButton.GetComponent<RectTransform>().position = new Vector2(finishButtonPosition.x, finishButtonPosition.y);
        startScreen.GetComponent<RectTransform>().position = new Vector2(startScreen.GetComponent<RectTransform>().position.x + 100, startScreen.GetComponent<RectTransform>().position.y);
        NextTurn();
    }

    public void NextTurn()
    {
        GetComponent<AudioSource>().Play();
        for (int i = 0; i < blocksVelocity.Length; i++)
        {
            if (blocksVelocity[i].GetComponent<Transform>().position.y + 2.5f > maxHeight)
            {
                maxHeight = blocksVelocity[i].GetComponent<Transform>().position.y + 2.5f;
            }
        }
        Debug.Log(maxHeight);
        blockTotal++;
        //reset scripts
        GetComponent<DisableDragging>().Disable();
        GetComponent<DragAndDrop>().ResetMoved();
        ground.GetComponent<BlockDetectorSinglePlayer>().SetDetecting();

        //ADD SET HEIGHT REACHED AND ADD TO BLOCKS PLACED
        Instantiate(blocks[shapeValue, colourValue], new Vector2(platform.GetComponent<Transform>().position.x, platform.GetComponent<Transform>().position.y), Quaternion.identity);

        colourValue++;
        if (colourValue > 3)
            colourValue = 0;
        shapeValue = Random.Range(0, 4);
    }


    public void EndGame()
    {
        EndScreen.GetComponent<RectTransform>().position = new Vector2(endScreenPosition.x, EndScreen.GetComponent<RectTransform>().position.y);
        finishButton.GetComponent<RectTransform>().position = new Vector2(finishButtonPosition.x + 100, finishButton.GetComponent<RectTransform>().position.y);

        maxHeight = Mathf.Round(maxHeight * 10.0f) * 0.1f;
        heightReached.GetComponent<Text>().text = ("" + maxHeight + "m");
        blocksPlaced.GetComponent<Text>().text = ("" + blockTotal);
    }
}

