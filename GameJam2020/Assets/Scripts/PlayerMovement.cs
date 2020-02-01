using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Body parts:
    private bool normalLeg;
    private bool torso;
    private bool forkArm;
    private bool rocketLeg;

    private bool grounded = true;

    private float distanceToGround;
    private float horizontalInput;
    private float jumpForce = 10;

    private Rigidbody playerRigidbody;

    void Start()
    {
        distanceToGround = GetComponentInChildren<Collider>().bounds.extents.y;

        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        Movement();

        if (Input.GetKeyDown(KeyCode.Space) && CheckIfGrounded())
        {
            Jump();
        }
    }

    private bool CheckIfGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.1f))
            return true;
        else
            return false;
    }

    private void Movement()
    {
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime);
    }

    private void Jump()
    {
        grounded = false;
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
