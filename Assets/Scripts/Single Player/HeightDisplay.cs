using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeightDisplay : MonoBehaviour
{
    //object that displays height in text
    [SerializeField] private GameObject heightText;

    private float yPos;

    private Text text;

    private void Start()
    {
        GetComponent<Transform>().position = new Vector2(0f, 0f);
        text = heightText.GetComponent<Text>();
    }
    private void Update()
    {
        yPos = GetComponent<Transform>().position.y;
        yPos = Mathf.Round((yPos) * 10.0f) * 0.1f;
        text.text = ("View Height: " + yPos + "m");
    }
}
