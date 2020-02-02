using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrone : MonoBehaviour
{
    private bool detected;

    private float repeatTime;
    private float start;

    private int i;

    public BoxCollider[] colliders;
    public Light[] spotLights;

    void Start()
    {
        detected = false;

        repeatTime = 10f;
        start = 0f;

        i = 0;

        InvokeRepeating("ChangeLight", start, repeatTime);
    }

    void Update()
    {
        spotLights[i].intensity = 20f;
    }

    private void ChangeLight()
    {
        if (i == 0 && !detected)
        {
            spotLights[i].intensity = 0f;
            i++;
        }
        else if (i == 1 && !detected)
        {
            spotLights[i].intensity = 0f;
            i--;
        }
    }

    public void ChangeLightColor(int p)
    {
        if (p == i)
        {
            spotLights[i].color = new Color(1, 0, 0);
            detected = false;
            GameObject.Find("Player").GetComponent<PlayerMovement>().Freeze();
            Invoke("Kill", 1f);
        }
    }

    private void Kill()
    {
        GameObject.Find("Player").GetComponent<PlayerMovement>().Respawn();
        GameObject.Find("Player").GetComponent<PlayerMovement>().Freeze();
        spotLights[i].color = Color.yellow;

    }
}
