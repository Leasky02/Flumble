using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetector : MonoBehaviour
{
    public string cameraType;
    public GameObject cameraObject;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("block"))
        {
            if(collision.GetComponent<DragAndDrop>().useable)
            {
                if (cameraType == "down")
                    cameraObject.GetComponent<CameraMovement>().MoveCamDown();

                if (cameraType == "up")
                    cameraObject.GetComponent<CameraMovement>().MoveCamUp();

                if (cameraType == "left")
                    cameraObject.GetComponent<CameraMovement>().MoveCamLeft();

                if (cameraType == "right")
                    cameraObject.GetComponent<CameraMovement>().MoveCamRight();
            }
        }
    }
}
