using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkTargetScipt : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Fork")
        {
            if (GameObject.Find("Player").GetComponent<PlayerMovement>().forkShot)
            {
                GameObject.Find("Player").GetComponent<PlayerMovement>().forkHit = true;
                GameObject.Find("Player").GetComponent<PlayerMovement>().forkHitPosition = other.GetComponent<Transform>().position;
            }
        }
    }
}
