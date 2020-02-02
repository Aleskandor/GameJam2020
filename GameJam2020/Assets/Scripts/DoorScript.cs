using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    bool open = false;
    bool still = true;

    float doorSpeed = 2f;

    void Update()
    {
        if (open && !still)
        {
            transform.Translate(Vector3.up * Time.deltaTime * doorSpeed);
        }
        else if (!open && !still)
        {
            transform.Translate(Vector3.down * Time.deltaTime * doorSpeed);
        }
    }

    public void Door()
    {
        if (open)
        {
            open = false;
            DoorTimer();
        }
        else
        {
            open = true;
            DoorTimer();
        }
    }

    private void DoorTimer()
    {
        if (still)
        {
            still = false;
            Invoke("DoorTimer", 3f);
        }
        else
        {
            still = true;
        }
    }
}
