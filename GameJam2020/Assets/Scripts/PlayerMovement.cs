using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool forkArm;
    private bool grounded;
    private bool normalLeg;
    private bool rocketBoost;
    private bool rocketLeg;
    private bool torso;

    private float airSpeed;
    private float distanceToGround;
    private float downForce;
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

    private Rigidbody rigidbody;

    private Vector3 checkPoint;

    void Start()
    {
        forkArm = false;
        grounded = true;
        normalLeg = false;
        rocketBoost = false;
        rocketLeg = false;
        torso = false;

        airSpeed = 4f;
        distanceToGround = GetComponentInChildren<Collider>().bounds.extents.y;
        downForce = 0.1f;
        jumpForce = 10f;
        headSpeed = 7.5f;
        movementSpeed = headSpeed;
        legSpeed = 10f;
        rocketCharge = 1f;
        maxRocketCharge = 10f;

        rigidbody = GetComponent<Rigidbody>();

        checkPoint = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");

        Movement();

        if (CheckIfGrounded() && Input.GetKey(KeyCode.Space) && normalLeg)
        {
            if (rocketCharge < maxRocketCharge)
                rocketCharge += 100 * Time.deltaTime;
        }
        else if (Input.GetKeyUp(KeyCode.Space) && CheckIfGrounded() && normalLeg)
            Jump();
        else if (Input.GetKeyDown(KeyCode.Space) && rocketLeg && rocketBoost && !CheckIfGrounded())
            DoubleJump();
    }

    private bool CheckIfGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.01f))
        {
            if (stage1.activeSelf)
                movementSpeed = headSpeed;
            else
                movementSpeed = legSpeed;

            if (stage2.activeSelf && rigidbody.velocity.y < 0)
            {
                stage2.GetComponentInChildren<Animator>().SetBool("Jump", false);
                stage2.GetComponentInChildren<Animator>().SetBool("Land", true);
            }

            if (stage5.activeSelf)
            {
                rocketBoost = true;
            }

            return true;
        }
        else
        {
            if (rigidbody.velocity.y < 0)
                rigidbody.AddForce(Vector3.down * downForce, ForceMode.Impulse);

            movementSpeed = airSpeed;
            return false;
        }
    }

    private void Movement()
    {
        if (stage1.activeSelf)
            UpdateHeadMoveAnimationController();

        if (stage2.activeSelf && horizontalInput != 0)
        {
            stage2.GetComponentInChildren<Animator>().SetBool("Walk", true);

            if (horizontalInput < 0)
            {
                Quaternion targetRot = Quaternion.Euler(0, -90, 0);
                stage2.transform.rotation = Quaternion.RotateTowards(stage2.transform.rotation, targetRot, 10f);
            }
            else if (horizontalInput > 0)
            {
                Quaternion targetRot = Quaternion.Euler(0, 90, 0);
                stage2.transform.rotation = Quaternion.RotateTowards(stage2.transform.rotation, targetRot, 10f);
            }

        }
        else if (stage2.activeSelf)
        {
            stage2.GetComponentInChildren<Animator>().SetBool("Walk", false);
            Quaternion targetRot = Quaternion.Euler(0, 180, 0);
            stage2.transform.rotation = Quaternion.RotateTowards(stage2.transform.rotation, targetRot, 10f);
        }

        transform.Translate(Vector3.right * horizontalInput * movementSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if (stage2.activeSelf)
        {
            stage2.GetComponentInChildren<Animator>().SetBool("Land", false);
            stage2.GetComponentInChildren<Animator>().SetBool("Jump", true);
        }

        grounded = false;
        rigidbody.AddForce(Vector3.right * horizontalInput * jumpForce / 2, ForceMode.Impulse);
        rigidbody.AddForce(Vector3.up * (jumpForce + rocketCharge), ForceMode.Impulse);
        rocketCharge = 1f;
    }

    private void DoubleJump()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        rocketBoost = false;
    }

    private void Respawn()
    {
        transform.position = checkPoint;
        rigidbody.velocity = Vector3.zero;
    }

    private void UpdateHeadMoveAnimationController()
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
            Destroy(collision.gameObject);
            stage1.SetActive(false);
            stage2.SetActive(true);
            Camera.main.transform.position -= new Vector3(0, 0, 5);
            normalLeg = true;
            movementSpeed = legSpeed;
            distanceToGround = GetComponentInChildren<Collider>().bounds.extents.y;
        }
        else if (collision.gameObject.name == "Torso")
        {
            Destroy(collision.gameObject);
            stage2.SetActive(false);
            stage3.SetActive(true);
            torso = true;
            distanceToGround = GetComponentInChildren<Collider>().bounds.extents.y;
        }
        else if (collision.gameObject.name == "Fork")
        {
            Destroy(collision.gameObject);
            stage3.SetActive(false);
            stage4.SetActive(true);
            forkArm = true;
            distanceToGround = GetComponentInChildren<Collider>().bounds.extents.y;
        }
        else if (collision.gameObject.name == "Rocket")
        {
            Destroy(collision.gameObject);
            stage4.SetActive(false);
            stage5.SetActive(true);
            rocketLeg = true;
            rocketBoost = true;
            distanceToGround = GetComponentInChildren<Collider>().bounds.extents.y;
        }
        else if (collision.gameObject.CompareTag("CheckPoint"))
        {
            checkPoint = transform.position;
            Destroy(collision.gameObject);
        }
    }
}
