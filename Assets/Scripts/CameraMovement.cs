﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed;
    public void MoveCamDown()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.down * speed);
    }

    public void MoveCamUp()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * speed);
    }
    public void MoveCamLeft()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed);
    }
    public void MoveCamRight()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed);
    }
}