using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDetector : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("block"))
        {
            gameManager.GetComponent<FourPlayerStructure>().PlayerOut();
            //make it play particle effect
            //make it destroy object
        }
    }
}
