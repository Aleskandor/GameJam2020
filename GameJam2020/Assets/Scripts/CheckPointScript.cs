using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{

    public GameObject player;
    MeshRenderer meshR;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement pM = player.GetComponent<PlayerMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0); 
            
        }
    }
}
