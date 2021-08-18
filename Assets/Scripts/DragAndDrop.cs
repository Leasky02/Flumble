using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragAndDrop : MonoBehaviour 
{
    //variable that contains if an object is being dragged
    private bool dragging;

    private bool useable = true;
    //when the mouse is clicked down on the object
    private void OnMouseDown()
    {
        dragging = true;
        if(useable)
            GetComponent<Rigidbody2D>().gravityScale = 0;
    }
    //when the mouse is released on the object
    private void OnMouseUp()
    {
        dragging = false;
        if(useable)
            GetComponent<Rigidbody2D>().gravityScale = 10;
    }

    //do this every frame
    private void FixedUpdate()
    {
        //if dragging is active...
        if(dragging == true && useable == true)
        {
            //move the object towards the position of the mouse / screen touch
            Vector3 mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GetComponent<Rigidbody2D>().MovePosition(mouseposition);
        }
    }

    public void UnDraggable()
    {
        useable = false;
    }
}