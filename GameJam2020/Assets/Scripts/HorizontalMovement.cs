using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    private bool goingRight;

    private float speed;

    void Start()
    {
        goingRight = false;

        speed = 5f;

        Turn();
    }

    void Update()
    {
        if (goingRight)
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        else
            transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void Turn()
    {
        if(goingRight)
            goingRight = false;
        else
            goingRight = true;

        Invoke("Turn", 2f);
    }



 
}
