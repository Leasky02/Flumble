using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinterScript : MonoBehaviour
{
    [SerializeField] private Sprite snowSprite;
    [SerializeField] private GameObject ground;
    [SerializeField] private GameObject snowParticles;

    [SerializeField] private bool startParticles;
    // Start is called before the first frame update
    void Awake()
    {
        string month = System.DateTime.Now.ToString("MMMM");

        if (month == "December")
        {
            //Debug.Log("its december");
            ground.GetComponent<SpriteRenderer>().sprite = snowSprite;

            if (startParticles)
            {
                snowParticles.GetComponent<ParticleSystem>().Play();
            }
        }
        else
        {
            //Debug.Log("its not christmas");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
