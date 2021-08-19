using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FourPlayerStructure : MonoBehaviour
{
    //variables containing active and uppcoming players
    private int nextPlayer;
    private int currentPlayer;
    private int playerLoss;
    //time from player finishing turn to when they actually finish (dont go out for dropping block)
    public float timeBetweenTurns;

    //determines if each player is still in or out
    private bool oneOut = false;
    private bool twoOut = false;
    private bool threeOut = false;
    private bool fourOut = false;

    //leaderboards array
    public string[] leaderboard = new string[4];
    //sets first person out into value 3 in array (4th)
    public int outPosition = 3;

    [SerializeField] private GameObject leaderboardScreen;
    [SerializeField] private GameObject[] leaderboardPositions;
    private Vector2 leaderboardLocation;

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
    private Vector2 screenPosition;
    private Vector2 buttonPosition;

    public void Start()
    {
        shapeValue = Random.Range(0, 4);
        //saves position that elements start in
        screenPosition = readyScreen.GetComponent<Transform>().position;
        buttonPosition = finishButton.GetComponent<Transform>().position;
        leaderboardLocation = leaderboardScreen.GetComponent<Transform>().position;

        //sets player 1 to begin
        currentPlayer = 1;
        playerLoss = 1;
        nextPlayer = 1;
        //puts UI elements into position
        readyScreen.GetComponent<Transform>().position = screenPosition;
        finishButton.GetComponent<Transform>().position = new Vector2(0f, 20f);
        leaderboardScreen.GetComponent<RectTransform>().position = new Vector2(0f, 20f);

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
    }

    //begins players turn
    public void NextTurn()
    {
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
        readyScreen.GetComponent<Transform>().position = new Vector2(0f, 20f);
        finishButton.GetComponent<Transform>().position = buttonPosition;
    }

    //finished turn 
    public void EndTurn()
    {
        //sets UI in place
        readyScreen.GetComponent<Transform>().position = screenPosition;
        finishButton.GetComponent<Transform>().position = new Vector2(0f, 20f);

        switch(nextPlayer)
        {
            case 1:
                readyScreen.GetComponentInChildren<Text>().text = "Player 1\nTap when ready...";
                break;

            case 2:
                readyScreen.GetComponentInChildren<Text>().text = "Player 2\nTap when ready...";
                break;

            case 3:
                readyScreen.GetComponentInChildren<Text>().text = "Player 3\nTap when ready...";
                break;

            case 4:
                readyScreen.GetComponentInChildren<Text>().text = "Player 4\nTap when ready...";
                break;
        }
        GetComponent<DisableDragging>().Disable();

        //REMOVE COMPONENT OF ALL DRAG AND DROP
    }

    //player 1 turn
    public void Player1()
    {
        if (!oneOut)
        {
            currentPlayer = 1;
            nextPlayer = 2;
            Invoke("SwitchPlayerLoss", timeBetweenTurns);
            Instantiate(blocks[shapeValue, 0], new Vector2(platform.GetComponent<Transform>().position.x, platform.GetComponent<Transform>().position.y + 2), Quaternion.identity);
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
            Invoke("SwitchPlayerLoss", timeBetweenTurns);
            Instantiate(blocks[shapeValue, 1], new Vector2(platform.GetComponent<Transform>().position.x, platform.GetComponent<Transform>().position.y + 2), Quaternion.identity);
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
            Invoke("SwitchPlayerLoss", timeBetweenTurns);
            Instantiate(blocks[shapeValue, 2], new Vector2(platform.GetComponent<Transform>().position.x, platform.GetComponent<Transform>().position.y + 2), Quaternion.identity);
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
            Invoke("SwitchPlayerLoss", timeBetweenTurns);
            Instantiate(blocks[shapeValue, 3], new Vector2(platform.GetComponent<Transform>().position.x, platform.GetComponent<Transform>().position.y + 2), Quaternion.identity);
            playerText[3].GetComponent<Animator>().Play("Player4Active");
        }
        else
        {
            Player1();
        }

        //changes what shape is used in next round
        shapeValue++;
        if (shapeValue == 4)
            shapeValue = 0;
    }
    //player goes out function
    public void PlayerOut()
    {
        switch (playerLoss)
        {
            case 1:
                {
                    oneOut = true;
                    leaderboard[outPosition] = "Player 1";
                    playerText[0].GetComponent<Text>().color = new Color(0.54f, 0.54f, 0.54f, 0.5f);
                    break;
                }
            case 2:
                {
                    twoOut = true;
                    leaderboard[outPosition] = "Player 2";
                    playerText[1].GetComponent<Text>().color = new Color(0.54f, 0.54f, 0.54f, 0.5f);
                    break;
                }
            case 3:
                {
                    threeOut = true;
                    leaderboard[outPosition] = "Player 3";
                    playerText[2].GetComponent<Text>().color = new Color(0.54f, 0.54f, 0.54f, 0.5f);
                    break;
                }
            case 4:
                {
                    fourOut = true;
                    leaderboard[outPosition] = "Player 4";
                    playerText[3].GetComponent<Text>().color = new Color(0.54f, 0.54f, 0.54f, 0.5f);
                    break;
                }
        }

        outPosition--;
        Debug.Log(outPosition);

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
        else
        {
            EndTurn();
        }
    }

    public void SwitchPlayerLoss()
    {
        playerLoss = currentPlayer;
    }
    
    //end the game
    public void EndGame()
    {
        //make it play sound
        leaderboardScreen.GetComponent<RectTransform>().position = leaderboardLocation;
        finishButton.GetComponent<Transform>().position = new Vector2(0f, 20f);

        leaderboardPositions[0].GetComponent<Text>().text = leaderboard[0];
        leaderboardPositions[1].GetComponent<Text>().text = leaderboard[1];
        leaderboardPositions[2].GetComponent<Text>().text = leaderboard[2];
        leaderboardPositions[3].GetComponent<Text>().text = leaderboard[3];
        Debug.Log("end game");
    }
} 
