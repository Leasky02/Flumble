using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditScrollScript : MonoBehaviour
{
    private bool rollCredits;
    public float force;

    void Update()
    {
        if(rollCredits)
            GetComponent<Rigidbody2D>().AddForce(transform.up * force);
    }

    public void QueueCredits(bool move)
    {
        rollCredits = move;
    }
}
