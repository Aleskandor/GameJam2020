using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractButton : MonoBehaviour
{
    public bool canPress = false;
    private DoorScript door;
    // Start is called before the first frame update
    void Start()
    {
        door = GetComponentInChildren<DoorScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canPress = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            canPress = false;

        }
    }

    public void Pressed()
    {
        transform.Translate(Vector3.left / 2);
        door.Door();
        canPress = false;
        Invoke("Return", 3f);
    }

    private void Return()
    {
        transform.Translate(Vector3.right / 2);
        
    }
}
