using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool normalLeg;
    private bool torso;
    private bool forkArm;
    private bool rocketLeg;
    private bool grounded;

    private float distanceToGround;
    private float horizontalInput;
    private float jumpForce;
    private float movementSpeed;
    private float headSpeed;
    private float legSpeed;
    private float rocketCharge;
    private float maxRocketCharge;

    public GameObject stage1;
    public GameObject stage2;
    public GameObject stage3;
    public GameObject stage4;
    public GameObject stage5;

    private Rigidbody playerRigidbody;

    private Vector3 savePos;

    void Start()
    {
        normalLeg = false;
        torso = false;
        forkArm = false;
        rocketLeg = false;
        grounded = true;

        distanceToGround = GetComponentInChildren<Collider>().bounds.extents.y;
        jumpForce = 10f;
        headSpeed = 1f;
        movementSpeed = headSpeed;
        legSpeed = 2f;
        rocketCharge = 1f;
        maxRocketCharge = 10f;

        playerRigidbody = GetComponent<Rigidbody>();
        savePos = transform.position;
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        Movement();

        if (Input.GetKey(KeyCode.Space) && CheckIfGrounded() && rocketLeg)
        {
            if (rocketCharge < maxRocketCharge)
                rocketCharge += 1 * Time.deltaTime;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && CheckIfGrounded() && normalLeg && !rocketLeg)
                Jump();
        else if (Input.GetKeyUp(KeyCode.Space) && CheckIfGrounded() && rocketLeg)
                RocketJump();


        if (Input.GetKeyDown(KeyCode.R))
        {
            ReSpawn();
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
        UpdateAnimationController();

        //Actual movement in horizontal direction.
        transform.Translate(Vector3.right * horizontalInput * movementSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        grounded = false;
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void RocketJump()
    {
        grounded = false;
        playerRigidbody.AddForce(Vector3.up * (jumpForce + rocketCharge), ForceMode.Impulse);
        rocketCharge = 1f;
    }

    private void ReSpawn()
    {
        transform.position = savePos;
    }
    //Used to update the animators for the different stages of the character model.
    private void UpdateAnimationController()
    {
        if (Input.GetKey(KeyCode.D) && stage1.activeSelf)
            stage1.GetComponentInChildren<Animator>().SetBool("MovingRight", true);
        else if (stage1.activeSelf)
            stage1.GetComponentInChildren<Animator>().SetBool("MovingRight", false);

        if (Input.GetKey(KeyCode.A) && stage1.activeSelf)
            stage1.GetComponentInChildren<Animator>().SetBool("MovingLeft", true);
        else if (stage1.activeSelf)
            stage1.GetComponentInChildren<Animator>().SetBool("MovingLeft", false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Leg")
        {
            collision.gameObject.SetActive(false);
            stage1.SetActive(false);
            stage2.SetActive(true);
            normalLeg = true;
            movementSpeed = legSpeed;
        }
        else if (collision.gameObject.name == "Torso")
        {
            collision.gameObject.SetActive(false);
            stage2.SetActive(false);
            stage3.SetActive(true);
            torso = true;
        }
        else if (collision.gameObject.name == "Fork")
        {
            collision.gameObject.SetActive(false);
            stage3.SetActive(false);
            stage4.SetActive(true);
            forkArm = true;
        }
        else if (collision.gameObject.name == "Rocket")
        {
            collision.gameObject.SetActive(false);
            stage4.SetActive(false);
            stage5.SetActive(true);
            rocketLeg = true;
        }

        else if (collision.gameObject.CompareTag("CheckPoint"))
        {
            savePos = transform.position;
            Destroy(collision.gameObject);
        }
    }
}
