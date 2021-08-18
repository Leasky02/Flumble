using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetector : MonoBehaviour
{
    public string cameraType;
    public GameObject character;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            if (cameraType == "down")
                character.GetComponent<CameraMovement>().MoveCamDown();

            if (cameraType == "up")
                character.GetComponent<CameraMovement>().MoveCamUp();
        }
    }
}
