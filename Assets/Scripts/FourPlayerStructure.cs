using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FourPlayerStructure : MonoBehaviour
{
    //variables containing active and uppcoming players
    private int nextPlayer;
    private int currentPlayer;

    //determines if each player is still in or out
    public bool oneOut = false;
    public bool twoOut = false;
    public bool threeOut = false;
    public bool fourOut = false;

    private bool setup = true;

    //leaderboards array
    public string[] leaderboard = new string[4];
    //sets first person out into value 3 in array (4th)
    public int outPosition = 3;

    [SerializeField] private GameObject leaderboardScreen;
    [SerializeField] private GameObject[] leaderboardPositions;
    private Vector2 leaderboardLocation;

    public GameObject ground;

    //spawn platform for blocks
    [SerializeField] private GameObject platform;

    [SerializeField] private GameObject[] playerText;

    //all shapes in arrays
    [SerializeField] private GameObject[,] blocks = new GameObject[4,4];
    [SerializeField] private GameObject[] squares;
    [SerializeField] private GameObject[] lines;
    [SerializeField] private GameObject[] triangles;
    [SerializeField] private GameObject[] circles;

    private int shapeValue = 0;

    //UI elements and positions
    [SerializeField] private GameObject readyScreen;
    [SerializeField] private GameObject finishButton;
    [SerializeField] private GameObject exitButton;

    //testing if there is still movement
    private bool stillMovement = false;
    private GameObject[] blocksVelocity;

    //if turn is over
    private bool turnOver = false;

    //screen positions
    private Vector2 screenPosition;
    private Vector2 buttonPosition;
    private Vector2 exitButtonPosition;

    private Color[] colours = new Color[4];

    //lives variables
    private int startingLives = 3;
    [SerializeField] private GameObject[] livesText;
    private int[] livesleft = new int[5];
    [SerializeField] private GameObject gameplayManager;

    public void Start()
    {
        startingLives = gameplayManager.GetComponent<GameplaySettings>().ReturnLives();
        shapeValue = Random.Range(0, 4);
        //saves position that elements start in
        screenPosition = readyScreen.GetComponent<Transform>().position;
        buttonPosition = finishButton.GetComponent<Transform>().position;
        exitButtonPosition = exitButton.GetComponent<Transform>().position;
        leaderboardLocation = leaderboardScreen.GetComponent<Transform>().position;

        //puts UI elements into position
        finishButton.GetComponent<Transform>().position = new Vector2(buttonPosition.x + 100 , finishButton.GetComponent<Transform>().position.y);
        exitButton.GetComponent<Transform>().position = new Vector2(exitButtonPosition.x + 100 , exitButton.GetComponent<Transform>().position.y);
        leaderboardScreen.GetComponent<RectTransform>().position = new Vector2(leaderboardLocation.x + 100, leaderboardScreen.GetComponent<RectTransform>().position.y);

        //set lives
        livesleft[1] = startingLives;
        livesleft[2] = startingLives;
        livesleft[3] = startingLives;
        livesleft[4] = startingLives;

        //set lives text
        livesText[1].GetComponent<Text>().text = ("" + startingLives);
        livesText[2].GetComponent<Text>().text = ("" + startingLives);
        livesText[3].GetComponent<Text>().text = ("" + startingLives);
        livesText[4].GetComponent<Text>().text = ("" + startingLives);

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

        //set colours
        colours[0] = new Color(222f/255f, 233f/255f, 48f/255f); //yellow
        colours[1] = new Color(65f/255f, 48f/255f, 233f/255f); //blue
        colours[2] = new Color(65f/255f, 233f/255f, 48f/255f); //green
        colours[3] = new Color(233f/255f, 48f/255f, 48f/255f); //red

        //if player 3 / 4 out (2/3 player game), put them out straight away
        if(threeOut)
        {
            currentPlayer = 3;
            PlayerOut();
        }
        if (fourOut)
        {
            currentPlayer = 4;
            PlayerOut();
        }

        //sets player 1 to begin
        currentPlayer = 1;
        nextPlayer = 1;

        //finish setup
        setup = false;

    }

    private void Update()
    {
        //test for all blocks if they are moving
        blocksVelocity = GameObject.FindGameObjectsWithTag("block");

        //tests all blocks if they are moving
        for (int i = 0; i < blocksVelocity.Length; i++)
        {
            if(blocksVelocity[i].GetComponent<Rigidbody2D>().velocity.magnitude > 0f)
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
        if (stillMovement || GetComponent<DragAndDrop>().IsItDragging() == true || GetComponent<DragAndDrop>().IsItMoved() == false || turnOver)
        {
            finishButton.GetComponent<Button>().interactable = false;
        }
        //if there is no movement, let it finish turn
        else if(!stillMovement)
        {
            finishButton.GetComponent<Button>().interactable = true;
        }
    }
    //begins players turn
    public void NextTurn()
    {
        turnOver = false;
        GetComponent<AudioSource>().Play();
        switch(nextPlayer)
        {
            case 1:
                Player1();
                break;
            case 2:
                Player2();
                break;
            case 3:
                Player3();
                break;
            case 4:
                Player4();
                break;
        }
        //puts UI elements in place
        readyScreen.GetComponent<Transform>().position = new Vector2(screenPosition.x + 100, readyScreen.GetComponent<Transform>().position.y);
        finishButton.GetComponent<Transform>().position = new Vector2(buttonPosition.x, finishButton.GetComponent<Transform>().position.y);
        exitButton.GetComponent<Transform>().position = new Vector2(exitButtonPosition.x, exitButton.GetComponent<Transform>().position.y);
        readyScreen.GetComponentInChildren<Button>().interactable = true;
        //reset if block has been moved
        GetComponent<DragAndDrop>().ResetMoved();
        //Start detecting for a failure
        ground.GetComponent<BlockDetector>().SetDetecting();
    }

    //finished turn 
    public void EndTurn()
    {
        //sets UI in place
        readyScreen.GetComponent<Transform>().position = new Vector2(screenPosition.x, readyScreen.GetComponent<Transform>().position.y);
        finishButton.GetComponent<Transform>().position = new Vector2(buttonPosition.x + 100, finishButton.GetComponent<Transform>().position.y);
        exitButton.GetComponent<Transform>().position = new Vector2(exitButtonPosition.x + 100, exitButton.GetComponent<Transform>().position.y);

        switch (nextPlayer)
        {
            case 1:
                if(!oneOut)
                    readyScreen.GetComponentInChildren<Text>().text = "Player 1\nTap when ready...";
                else if (!twoOut)
                    readyScreen.GetComponentInChildren<Text>().text = "Player 2\nTap when ready...";
                else if (!threeOut)
                    readyScreen.GetComponentInChildren<Text>().text = "Player 3\nTap when ready...";
                else if (!fourOut)
                    readyScreen.GetComponentInChildren<Text>().text = "Player 4\nTap when ready...";
                break;

            case 2:
                if (!twoOut)
                    readyScreen.GetComponentInChildren<Text>().text = "Player 2\nTap when ready...";
                else if (!threeOut)
                    readyScreen.GetComponentInChildren<Text>().text = "Player 3\nTap when ready...";
                else if (!fourOut)
                    readyScreen.GetComponentInChildren<Text>().text = "Player 4\nTap when ready...";
                else if (!oneOut)
                    readyScreen.GetComponentInChildren<Text>().text = "Player 1\nTap when ready...";
                break;

            case 3:
                if (!threeOut)
                    readyScreen.GetComponentInChildren<Text>().text = "Player 3\nTap when ready...";
                else if (!fourOut)
                    readyScreen.GetComponentInChildren<Text>().text = "Player 4\nTap when ready...";
                else if (!oneOut)
                    readyScreen.GetComponentInChildren<Text>().text = "Player 1\nTap when ready...";
                else if (!twoOut)
                    readyScreen.GetComponentInChildren<Text>().text = "Player 2\nTap when ready...";
                break;

            case 4:
                if (!fourOut)
                    readyScreen.GetComponentInChildren<Text>().text = "Player 4\nTap when ready...";
                else if (!oneOut)
                    readyScreen.GetComponentInChildren<Text>().text = "Player 1\nTap when ready...";
                else if (!twoOut)
                    readyScreen.GetComponentInChildren<Text>().text = "Player 2\nTap when ready...";
                else if (!threeOut)
                    readyScreen.GetComponentInChildren<Text>().text = "Player 3\nTap when ready...";
                break;
        }

        //disable dragging off block
        GetComponent<DisableDragging>().Disable();

    }

    //player 1 turn
    public void Player1()
    {
        if (!oneOut)
        {
            currentPlayer = 1;
            nextPlayer = 2;
            Instantiate(blocks[shapeValue, 0], new Vector2(platform.GetComponent<Transform>().position.x, platform.GetComponent<Transform>().position.y), Quaternion.identity);
            playerText[0].GetComponent<Animator>().Play("Player1Active");
        }
        else
        {
            Player2();
        }
    }
    //player 2 turn
    public void Player2()
    {
        if(!twoOut)
        {
            currentPlayer = 2;
            nextPlayer = 3;
            Instantiate(blocks[shapeValue, 1], new Vector2(platform.GetComponent<Transform>().position.x, platform.GetComponent<Transform>().position.y), Quaternion.identity);
            playerText[1].GetComponent<Animator>().Play("Player2Active");
        }
        else
        {
            Player3();
        }
    }
    //player 3 turn
    public void Player3()
    {
        if(!threeOut)
        {
            currentPlayer = 3;
            nextPlayer = 4;
            Instantiate(blocks[shapeValue, 2], new Vector2(platform.GetComponent<Transform>().position.x, platform.GetComponent<Transform>().position.y), Quaternion.identity);
            playerText[2].GetComponent<Animator>().Play("Player3Active");
        }
        else
        {
            Player4();
        }
    }
    //player 4 turn
    public void Player4()
    {
        if(!fourOut)
        {
            currentPlayer = 4;
            nextPlayer = 1;
            Instantiate(blocks[shapeValue, 3], new Vector2(platform.GetComponent<Transform>().position.x, platform.GetComponent<Transform>().position.y), Quaternion.identity);
            playerText[3].GetComponent<Animator>().Play("Player4Active");

            //changes what shape is used in next round
            shapeValue++;
            if (shapeValue == 4)
                shapeValue = 0;
        }
        else
        {

            //changes what shape is used in next round
            shapeValue++;
            if (shapeValue == 4)
                shapeValue = 0;

            Player1();
        }
    }

    //lose a life function
    public void LoseLives()
    {
        //remove a life and update text
        livesleft[currentPlayer]--;
        livesText[currentPlayer].GetComponent<Text>().text = ("" + livesleft[currentPlayer]);
        //if player is out
        if(livesleft[currentPlayer] <= 0)
        {
            PlayerOut();
        }
        else
        {
            EndTurn();
        }
    }


    //player goes out function
    public void PlayerOut()
    {
        //disable dragging
        GetComponent<DisableDragging>().Disable();
        //finish turn button cant be pressed
        turnOver = true;
        finishButton.GetComponent<Button>().interactable = false;

        switch (currentPlayer)
        {
            case 1:
                {
                    oneOut = true;
                    leaderboard[outPosition] = "Player 1";
                    playerText[0].GetComponent<Text>().color = new Color(0.54f, 0.54f, 0.54f, 0.5f);
                    livesText[currentPlayer].GetComponent<Text>().color = new Color(0.54f, 0.54f, 0.54f, 0.5f);
                    break;
                }
            case 2:
                {
                    twoOut = true;
                    leaderboard[outPosition] = "Player 2";
                    playerText[1].GetComponent<Text>().color = new Color(0.54f, 0.54f, 0.54f, 0.5f);
                    livesText[currentPlayer].GetComponent<Text>().color = new Color(0.54f, 0.54f, 0.54f, 0.5f);
                    break;
                }
            case 3:
                {
                    threeOut = true;
                    leaderboard[outPosition] = "Player 3";
                    playerText[2].GetComponent<Text>().color = new Color(0.54f, 0.54f, 0.54f, 0.5f);
                    livesText[currentPlayer].GetComponent<Text>().color = new Color(0.54f, 0.54f, 0.54f, 0.5f);
                    break;
                }
            case 4:
                {
                    fourOut = true;
                    leaderboard[outPosition] = "Player 4";
                    playerText[3].GetComponent<Text>().color = new Color(0.54f, 0.54f, 0.54f, 0.5f);
                    livesText[currentPlayer].GetComponent<Text>().color = new Color(0.54f, 0.54f, 0.54f, 0.5f);
                    break;
                }
        }

        outPosition--;
        //if 3 players are out, set leaderboard positions
        if(outPosition < 1)
        {
            if(!oneOut)
                leaderboard[outPosition] = "Player 1";
            if (!twoOut)
                leaderboard[outPosition] = "Player 2";
            if (!threeOut)
                leaderboard[outPosition] = "Player 3";
            if (!fourOut)
                leaderboard[outPosition] = "Player 4";

            EndGame();
        }
        else if(!setup)
        {
            Invoke("EndTurn", 1.5f);
        }
    }
    
    //end the game
    public void EndGame()
    {
        //make it play sound
        exitButton.GetComponent<Transform>().position = new Vector2(exitButtonPosition.x + 100, exitButton.GetComponent<Transform>().position.y);
        leaderboardScreen.GetComponent<RectTransform>().position = new Vector2(leaderboardLocation.x, leaderboardScreen.GetComponent<RectTransform>().position.y);
        finishButton.GetComponent<Transform>().position = new Vector2(buttonPosition.x + 100, finishButton.GetComponent<Transform>().position.y);
        //set leaderboards
        leaderboardPositions[0].GetComponent<Text>().text = leaderboard[0];
        leaderboardPositions[1].GetComponent<Text>().text = leaderboard[1];
        leaderboardPositions[2].GetComponent<Text>().text = leaderboard[2];
        leaderboardPositions[3].GetComponent<Text>().text = leaderboard[3];
        //set colours
        //1st
        if(leaderboard[0] == "Player 1")
        {
            leaderboardPositions[0].GetComponent<Text>().color = colours[0];
        }
        else if (leaderboard[0] == "Player 2")
        {
            leaderboardPositions[0].GetComponent<Text>().color = colours[1];
        }
        else if (leaderboard[0] == "Player 3")
        {
            leaderboardPositions[0].GetComponent<Text>().color = colours[2];
        }
        else if (leaderboard[0] == "Player 4")
        {
            leaderboardPositions[0].GetComponent<Text>().color = colours[3];
        }

        //2nd
        if (leaderboard[1] == "Player 1")
        {
            leaderboardPositions[1].GetComponent<Text>().color = colours[0];
        }
        else if (leaderboard[1] == "Player 2")
        {
            leaderboardPositions[1].GetComponent<Text>().color = colours[1];
        }
        else if (leaderboard[1] == "Player 3")
        {
            leaderboardPositions[1].GetComponent<Text>().color = colours[2];
        }
        else if (leaderboard[1] == "Player 4")
        {
            leaderboardPositions[1].GetComponent<Text>().color = colours[3];
        }

        //3rd
        if (leaderboard[2] == "Player 1")
        {
            leaderboardPositions[2].GetComponent<Text>().color = colours[0];
        }
        else if (leaderboard[2] == "Player 2")
        {
            leaderboardPositions[2].GetComponent<Text>().color = colours[1];
        }
        else if (leaderboard[2] == "Player 3")
        {
            leaderboardPositions[2].GetComponent<Text>().color = colours[2];
        }
        else if (leaderboard[2] == "Player 4")
        {
            leaderboardPositions[2].GetComponent<Text>().color = colours[3];
        }

        //4th
        if (leaderboard[3] == "Player 1")
        {
            leaderboardPositions[3].GetComponent<Text>().color = colours[0];
        }
        else if (leaderboard[3] == "Player 2")
        {
            leaderboardPositions[3].GetComponent<Text>().color = colours[1];
        }
        else if (leaderboard[3] == "Player 3")
        {
            leaderboardPositions[3].GetComponent<Text>().color = colours[2];
        }
        else if (leaderboard[3] == "Player 4")
        {
            leaderboardPositions[3].GetComponent<Text>().color = colours[3];
        }
    }
} 
