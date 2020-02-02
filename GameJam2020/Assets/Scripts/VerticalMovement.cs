using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovement : MonoBehaviour
{
    private bool goingUp = false;
    private float speed;
    Transform test;
    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
        Turn();
    }

    // Update is called once per frame
    void Update()
    {
        if (goingUp)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
    }

    private void Turn()
    {

        if (goingUp)
        {
            goingUp = false;

        }
        else
        {
            goingUp = true;
        }
        Invoke("Turn", 2f);
    }



}
