using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    //prefab containing cloud
    [SerializeField] private GameObject cloud;
    //parameters
    [SerializeField] private GameObject[] Parameters;

    private int clourNumber;
    // Start is called before the first frame update
    void Awake()
    {
        Instantiate(Parameters[0]);
        Instantiate(Parameters[1]);
        Instantiate(Parameters[2]);
        Instantiate(Parameters[3]);

        clourNumber = Random.Range(3, 30);
        for (int i = 0; i < clourNumber; i++)
        {
            Instantiate(cloud);

        }
    }
}
