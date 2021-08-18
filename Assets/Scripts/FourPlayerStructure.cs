using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FourPlayerStructure : MonoBehaviour
{
    //variables containing active and uppcoming players
    private int nextPlayer;
    private int currentPlayer;

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

        //sets player 1 to begin
        currentPlayer = 0;
        nextPlayer = 1;
        //puts UI elements into position
        readyScreen.GetComponent<Transform>().position = screenPosition;
        finishButton.GetComponent<Transform>().position = new Vector2(0f, 20f);

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
        currentPlayer = 1;
        nextPlayer = 2;
        Instantiate(blocks[shapeValue,0], new Vector2(platform.GetComponent<Transform>().position.x, platform.GetComponent<Transform>().position.y+2), Quaternion.identity);
        playerText[0].GetComponent<Animator>().Play("Player1Active");
    }
    //player 2 turn
    public void Player2()
    {
        currentPlayer = 2;
        nextPlayer = 3;
        Instantiate(blocks[shapeValue, 1], new Vector2(platform.GetComponent<Transform>().position.x, platform.GetComponent<Transform>().position.y+2), Quaternion.identity);
        playerText[1].GetComponent<Animator>().Play("Player2Active");
    }
    //player 3 turn
    public void Player3()
    {
        currentPlayer = 3;
        nextPlayer = 4;
        Instantiate(blocks[shapeValue, 2], new Vector2(platform.GetComponent<Transform>().position.x, platform.GetComponent<Transform>().position.y+2), Quaternion.identity);
        playerText[2].GetComponent<Animator>().Play("Player3Active");
    }
    //player 4 turn
    public void Player4()
    {
        currentPlayer = 4;
        nextPlayer = 1;
        Instantiate(blocks[shapeValue, 3], new Vector2(platform.GetComponent<Transform>().position.x, platform.GetComponent<Transform>().position.y+2), Quaternion.identity);
        playerText[3].GetComponent<Animator>().Play("Player4Active");

        //changes what shape is used in next round
        shapeValue++;
        if (shapeValue == 4)
            shapeValue = 0;
    }
} 
