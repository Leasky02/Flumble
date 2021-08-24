using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSimulation : MonoBehaviour
{
    [SerializeField] private GameObject[] squares;
    [SerializeField] private GameObject[] circles;
    [SerializeField] private GameObject[] triangles;
    [SerializeField] private GameObject[] lines;
    [SerializeField] private GameObject[,] blocks = new GameObject[4,4];

    [SerializeField] private GameObject[] allBlocks;

    private int randomShape;
    private int colourValue = 0;
    private float randomXPosition;
    private float randomTime;
    // Start is called before the first frame update
    void Start()
    {
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

        //set random time
        randomTime = Random.Range(0.5f, 4f);
        //invoke creating a shape
        Invoke("CreateObject", randomTime);
    }

    void CreateObject()
    {
        //setting all random variables
        randomShape = Random.Range(0, 4);
        randomXPosition = Random.Range(2f, 5.1f);
        colourValue++;
        if (colourValue > 3)
            colourValue = 0;
        randomTime = Random.Range(0.5f, 4f);
        //create object
        Instantiate(blocks[randomShape, colourValue] , new Vector2(randomXPosition , 8f), Quaternion.identity);
        //queue next object spawning
        Invoke("CreateObject", randomTime);

        //finding all blocks and remove sound effects script
        allBlocks = GameObject.FindGameObjectsWithTag("block");
        for (int i = 0; i < allBlocks.Length; i++)
        {
            Destroy(allBlocks[i].GetComponent<SoundEffects>());
        }
    }
}
