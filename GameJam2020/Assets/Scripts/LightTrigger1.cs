using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger1 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponentInParent<EnemyDrone>().ChangeLightColor(1);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponentInParent<EnemyDrone>().ChangeLightColor(1);
        }
    }
}
