using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMovement : MonoBehaviour
{
    private Vector2 newPosition;
    private RectTransform rt;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        rb = GetComponent<Rigidbody2D>();
        newPosition = new Vector2(rt.position.x, rt.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        rt.position = Vector2.MoveTowards(rt.position, newPosition, 1f);
    }

    public void MoveLeft()
    {
        GetComponent<AudioSource>().Play();
        newPosition = new Vector2(rt.position.x-30, rt.position.y);
    }

    public void MoveRight()
    {
        GetComponent<AudioSource>().Play();
        newPosition = new Vector2(rt.position.x + 30, rt.position.y);
    }
}
