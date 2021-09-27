using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DragAndDrop : MonoBehaviour 
{
    [SerializeField] private bool unusable;
    public static bool moved = false;
    public static float size = 0.2f;
    public bool active = false;
    private string currentScene;

    public bool usingThisObject;

    //variable that contains if an object is being dragged
    static bool dragging;
    //if script should be used
    public bool useable = true;

    private bool delete = true;

    //if it is released
    private bool released = false;

    public bool canBeDestroyed = false;

    //object containing colour blind more
    [SerializeField] private GameObject colourBlindText;

    //bin audio clip
    [SerializeField] private AudioClip binSound;

    //colourblind
    public static bool colourBlindMode;


    private void Awake()
    {
        if (PlayerPrefs.GetInt("colourBlindMode") == 0)
        {
            colourBlindMode = false;
        }
        else if (PlayerPrefs.GetInt("colourBlindMode") == 1)
        {
            colourBlindMode = true;
        }
    }

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == ("Free Play"))
        {
            GetComponent<Transform>().localScale = new Vector2(size, size);
            if(!unusable)
                GetComponent<Rigidbody2D>().gravityScale = 5;
        }
        else if (currentScene == ("Main Menu"))
        {
            if (!unusable)
                GetComponent<Rigidbody2D>().gravityScale = 5;
                useable = false;
        }
        else
        {
            float randomScale = Random.Range(0.2f, 0.4f);
            GetComponent<Transform>().localScale = new Vector2(randomScale, randomScale);
        }

        //test if colour blind mode is on
        if (currentScene != ("1 Player") && currentScene != ("Free Play") && currentScene != ("Main Menu"))
        {
            if (!unusable)
            {
                //if colour blind mode is on, set number to true
                if (colourBlindMode == true)
                {
                    //Debug.Log(colourBlindMode);
                    colourBlindText.GetComponent<SpriteRenderer>().enabled = true;
                    //Debug.Log("true");
                }
                else
                {
                    colourBlindText.GetComponent<SpriteRenderer>().enabled = false;
                    //Debug.Log("false");
                }
            }
        }
    }
    private void OnMouseDown()
    {
        //Debug.Log("clicked");
        if (useable)
        {
            usingThisObject = true;
            moved = true;
            canBeDestroyed = false;
            dragging = true;
            active = true;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            released = false;
        }
    }
    //when the mouse is released on the object
    private void OnMouseUp()
    {
        if(useable)
        {
            usingThisObject = false;
            dragging = false;
            canBeDestroyed = true;
            active = false;
            GetComponent<Rigidbody2D>().gravityScale = 5;
            released = true;
            Invoke("SetReleased", 0.1f);
        }
    }

    //do this every frame
    private void FixedUpdate()
    {
        //if dragging is active...
        if(dragging == true && useable == true)
        {
            if(!unusable && usingThisObject)
            {
                //move the object towards the position of the mouse / screen touch
                if (Input.touchCount > 0)
                {
                    Vector3 mouseposition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    GetComponent<Rigidbody2D>().MovePosition(mouseposition);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("delete"))
        {
            if (released && delete)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                Destroy(gameObject.GetComponent<BoxCollider2D>());
                GetComponent<AudioSource>().clip = binSound;
                GetComponent<AudioSource>().Play();
                Destroy(gameObject, 0.5f);
                delete = false;
                Invoke("SetDelete", 0.1f);
            }
        }

        if (collision.CompareTag("ground"))
        {
            //Debug.Log("hit");
            colourBlindText.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void SetDelete()
    {
        delete = true;
    }

    public void UnDraggable()
    {
        canBeDestroyed = true;
        useable = false;
        colourBlindText.GetComponent<SpriteRenderer>().enabled = false;
    }
    public bool IsItDragging()
    {
        return dragging;
    }

    public bool IsItMoved()
    {
        return moved;
    }

    public void ResetMoved()
    {
        moved = false;
    }

    public void SetSize(float sizeValue)
    {
        size = sizeValue;
    }

    public void SetReleased()
    {
        released = false;
    }


    public void ChangeColourBlindMode(bool mode)
    {
        colourBlindMode = mode;
        //Debug.Log(mode);
        if (!mode)
            PlayerPrefs.SetInt("colourBlindMode", 0);
        if (mode)
            PlayerPrefs.SetInt("colourBlindMode", 1);
    }

    public bool ReturnMode()
    {
        return colourBlindMode;
    }
}
