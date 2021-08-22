﻿using UnityEngine.UI;
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

    private bool usingThisObject;

    //variable that contains if an object is being dragged
    static bool dragging;

    public bool useable = true;



    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == ("Free Play"))
        {
            GetComponent<Transform>().localScale = new Vector2(size, size);
            if(!unusable)
                GetComponent<Rigidbody2D>().gravityScale = 5;
        }
        else
        {
            float randomScale = Random.Range(0.2f, 0.4f);
            GetComponent<Transform>().localScale = new Vector2(randomScale, randomScale);
        }


    }
    private void OnMouseDown()
    {
        if (useable)
        {
            usingThisObject = true;
            dragging = true;
            active = true;
            moved = true;
            GetComponent<Rigidbody2D>().gravityScale = 5;
        }
    }
    //when the mouse is released on the object
    private void OnMouseUp()
    {
        if(useable)
        {
            usingThisObject = false;
            dragging = false;
            active = false;
            GetComponent<Rigidbody2D>().gravityScale = 5;
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
                Vector3 mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GetComponent<Rigidbody2D>().MovePosition(mouseposition);
            }
        }
    }

    public void UnDraggable()
    {
        useable = false;
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
}
