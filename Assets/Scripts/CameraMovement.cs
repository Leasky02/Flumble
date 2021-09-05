using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed;
    public void MoveCamDown(int multiplier)
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.down * speed * multiplier);
    }

    public void MoveCamUp(int multiplier)
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * speed * 2);
    }
    public void MoveCamLeft(int multiplier)
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed * multiplier);
    }
    public void MoveCamRight(int multiplier)
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed * multiplier);
    }
}
