using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableDragging : MonoBehaviour
{
    private GameObject[] blocks;

    public void Disable()
    {
        blocks = GameObject.FindGameObjectsWithTag("block");

        for (int i = 0; i < blocks.Length; i++) 
        {
            blocks[i].GetComponent<DragAndDrop>().UnDraggable();
        }

    }
}
