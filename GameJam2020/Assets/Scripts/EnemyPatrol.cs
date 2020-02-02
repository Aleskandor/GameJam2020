using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Light light;
    Quaternion targetRotation;
    float speed = 3f;
    float time = 0;
    bool turning = false;
    bool left = false;
    bool setTarget = false;

    void Update()
    {
        if (!turning)
        {
            if(left)
                transform.Translate(-transform.right * speed * Time.deltaTime);
            else if(!left)
                transform.Translate(transform.right * speed * Time.deltaTime);

            time += Time.deltaTime;

            if (time >= 4f)
                turning = true;
        }
        else if (turning)
        {
            if (!setTarget)
            {
                if (left)
                {
                    targetRotation = Quaternion.Euler(0, 0, 0);
                    left = false;
                }
                else if (!left)
                {
                    targetRotation = Quaternion.Euler(0, 180, 0);
                    left = true;
                }
                setTarget = true;
            }

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 3f);

            if (transform.rotation.eulerAngles.y == targetRotation.eulerAngles.y)
            {
                time = 0;
                turning = false;
                setTarget = false;
            }
        }    
    }

    public void GotCaught()
    {
        light.color = Color.red;
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().Freeze();
        Invoke("Kill", 1f);
    }

    private void Kill()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().Respawn();
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().Freeze();
        light.color = Color.yellow;
    }
}
