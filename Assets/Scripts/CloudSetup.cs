using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSetup : MonoBehaviour
{
    //controls speed of cloud
    private float speed;
    private int cloudClass;
    private float scale;

    //cloud spawn positions
    [SerializeField] private GameObject[] x_Parameters;
    [SerializeField] private GameObject[] y_Parameters;

    private Vector2 cloudSpawnPosition;

    //cloud sprites
    [SerializeField] private Sprite[] cloudSprites;
    private int randomSprite;
    // Start is called before the first frame update
    void Start()
    {
        //sets cloud in random class
        cloudClass = Random.Range(1, 4);

        //sets scale
        scale = Random.Range(0.3f, 1.1f);

        //sets sprite random
        randomSprite = Random.Range(0, 3);
        switch (cloudClass)
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

        //sets sprite to random
        GetComponent<SpriteRenderer>().sprite = cloudSprites[randomSprite];

        //sets sprite location coordinates
        cloudSpawnPosition.x = Random.Range(x_Parameters[0].GetComponent<Transform>().position.x, x_Parameters[1].GetComponent<Transform>().position.x);
        cloudSpawnPosition.y = Random.Range(y_Parameters[0].GetComponent<Transform>().position.y, y_Parameters[1].GetComponent<Transform>().position.y);
        //move object to start point
        GetComponent<Transform>().position = cloudSpawnPosition;

        //sets scale of cloud
        GetComponent<Transform>().localScale = new Vector2(scale, scale);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed);
    }

    public void ClassOne()
    {
        //sets cloud speed
        speed = 0.3f;
        //sets alpha value
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.9f);
    }

    public void ClassTwo()
    {
        //sets cloud speed
        speed = 0.2f;
        //sets alpha value
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.6f);
    }

    public void ClassThree()
    {
        //sets cloud speed
        speed = 0.1f;
        //sets alpha value
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("cloud"))
        {
            //Debug.Log("collide");
            GetComponent<Transform>().position = new Vector2(x_Parameters[0].GetComponent<Transform>().position.x, GetComponent<Transform>().position.y);
        }
    }

    public void GetParameters(GameObject[] x_param, GameObject[] y_param)
    {
        x_Parameters[0] = x_param[0];
        x_Parameters[1] = x_param[1];
        y_Parameters[0] = y_param[0];
        y_Parameters[1] = y_param[1];
    }
}