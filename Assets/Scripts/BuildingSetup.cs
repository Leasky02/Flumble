using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSetup : MonoBehaviour
{
    //controls speed of cloud
    private int buildingClass;
    private float scale;

    //cloud spawn positions
    [SerializeField] private GameObject[] x_Parameters;

    private Vector2 buildingSpawnPosition;

    //cloud sprites
    [SerializeField] private Sprite[] buildingSprites;
    private int randomSprite;
    // Start is called before the first frame update
    void Start()
    {
        //sets cloud in random class
        buildingClass = Random.Range(1, 4);

        //sets sprite random
        randomSprite = Random.Range(0, 8);

        //sets sprite to random
        GetComponent<SpriteRenderer>().sprite = buildingSprites[randomSprite];

        //sets sprite location coordinates
        buildingSpawnPosition.x = Random.Range(x_Parameters[0].GetComponent<Transform>().position.x, x_Parameters[1].GetComponent<Transform>().position.x);
        switch (buildingClass)
        {
            case 1:
                ClassOne();
                break;
            case 2:
                ClassTwo();
                break;
            case 3:
                ClassThree();
                break;
        }
    }

    public void ClassOne()
    {
        //sets alpha value
        GetComponent<SpriteRenderer>().sortingOrder = -25;
        scale = Random.Range(0.8f, 1.5f);
        GetComponent<Transform>().localScale = new Vector2(scale, scale);

        buildingSpawnPosition.y = -2f;
        //move object to start point
        GetComponent<Transform>().position = buildingSpawnPosition;
    }

    public void ClassTwo()
    {
        //sets alpha value
        GetComponent<SpriteRenderer>().sortingOrder = -30;
        scale = Random.Range(0.6f, 1.6f);
        GetComponent<Transform>().localScale = new Vector2(scale, scale);

        buildingSpawnPosition.y = -2.3f;
        //move object to start point
        GetComponent<Transform>().position = buildingSpawnPosition;
    }

    public void ClassThree()
    {
        //sets alpha value
        GetComponent<SpriteRenderer>().sortingOrder = -35;
        scale = Random.Range(0.8f, 1.4f);
        GetComponent<Transform>().localScale = new Vector2(scale, scale);

        buildingSpawnPosition.y = -2.8f;
        //move object to start point
        GetComponent<Transform>().position = buildingSpawnPosition;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("cloud"))
        {
            GetComponent<Transform>().position = new Vector2(x_Parameters[0].GetComponent<Transform>().position.x, GetComponent<Transform>().position.y);
        }
    }

    public void GetParameters(GameObject[] x_param, GameObject[] y_param)
    {
        x_Parameters[0] = x_param[0];
        x_Parameters[1] = x_param[1];
    }
}
