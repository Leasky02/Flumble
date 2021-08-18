using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed;
    public int jumpHeight;
    public bool leftButtonPressed;
    public bool rightButtonPressed;
    private bool onGround;

    private void Update()
    {
        //Walk Right
        if (Input.GetKeyDown(KeyCode.D))
        {
            if(!leftButtonPressed)
            {
                rightButtonPressed = true;
                GetComponent<Animator>().Play("WalkingRight");
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if(!leftButtonPressed)
            {
                if(onGround)
                    GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed);
                else
                    GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed / 2);
            }
            
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            if(!leftButtonPressed)
            {
                rightButtonPressed = false;

                if (Input.GetKey(KeyCode.A))
                {
                    GetComponent<Animator>().Play("WalkingLeft");
                }
                else
                {
                    GetComponent<Animator>().Play("idle");
                }
            }
        }

        //Walk Left
        if (Input.GetKeyDown(KeyCode.A))
        {
            if(!rightButtonPressed)
            {
                leftButtonPressed = true;
                GetComponent<Animator>().Play("WalkingLeft");
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if(!rightButtonPressed)
            {
                if (onGround)
                    GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed);
                else
                    GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed / 2);
            }

        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            if (!rightButtonPressed)
            {
                leftButtonPressed = false;

                if(Input.GetKey(KeyCode.D))
                {
                    GetComponent<Animator>().Play("WalkingRight");
                }
                else
                {
                    GetComponent<Animator>().Play("idle");
                }
            }
        }

        //jump
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (onGround)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpHeight);
            }
        }
    }

    public void OnGround()
    {
        onGround = true;
    }

    public void OffGround()
    {
        onGround = false;
    }
}
