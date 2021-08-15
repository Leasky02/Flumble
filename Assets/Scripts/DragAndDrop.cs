using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragAndDrop : MonoBehaviour 
{
    //variable that contains if an object is being dragged
    private bool dragging;
    //when the mouse is clicked down on the object
    private void OnMouseDown()
    {
        dragging = true;
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }
    //when the mouse is released on the object
    private void OnMouseUp()
    {
        dragging = false;
        GetComponent<Rigidbody2D>().gravityScale = 2;
    }

    //do this every frame
    private void FixedUpdate()
    {
        //if dragging is active...
        if(dragging == true)
        {
            //move the object towards the position of the mouse / screen touch
            Vector3 mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GetComponent<Rigidbody2D>().MovePosition(mouseposition);
        }
    }
}