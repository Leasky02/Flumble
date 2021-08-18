using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public GameObject character;
    private void OnTriggerStay2D(Collider2D collision)
    {
        character.GetComponent<PlayerMovement>().OnGround();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        character.GetComponent<PlayerMovement>().OffGround();
    }
}
