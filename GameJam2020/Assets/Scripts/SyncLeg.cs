using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncLeg : MonoBehaviour
{
    public Transform leg;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -leg.rotation.z);
    }
}
