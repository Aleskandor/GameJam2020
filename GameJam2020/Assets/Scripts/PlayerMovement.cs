﻿using System.Collections;
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
    private bool upBoost;

    private float airSpeed;
    private float distanceToGround;
    private float downForce;
    private float horizontalInput;
    private float jumpForce;
    private float movementSpeed;
    private float originalUpForce;
    private float headSpeed;
    private float legSpeed;
    private float upForce;

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
        upBoost = false;

        airSpeed = 5f;
        distanceToGround = GetComponentInChildren<Collider>().bounds.extents.y;
        downForce = 0.5f;
        jumpForce = 5f;
        headSpeed = 7.5f;
        movementSpeed = headSpeed;
        originalUpForce = 0.3f;
        legSpeed = 10f;
        upForce = 0.3f;

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

        if (CheckIfGrounded() && Input.GetKeyDown(KeyCode.Space) && normalLeg)
            Jump();
        if (Input.GetKey(KeyCode.Space) && !CheckIfGrounded() && normalLeg && upBoost)
        {
            rigidbody.AddForce(Vector3.up * upForce * 1.7f, ForceMode.Impulse);
            upForce -= Time.deltaTime * 5f;

            if (upForce <= 0)
            {
                upForce = originalUpForce;
                upBoost = false;
            }
        }
        else if (!Input.GetKey(KeyCode.Space) && !CheckIfGrounded() && normalLeg)
            rigidbody.AddForce(Vector3.down * downForce, ForceMode.Impulse);
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

            if (stage3.activeSelf && rigidbody.velocity.y < 0)
            {
                stage3.GetComponentInChildren<Animator>().SetBool("Jump", false);
                stage3.GetComponentInChildren<Animator>().SetBool("Land", true);
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
            stage2.GetComponentInChildren<Animator>().SetBool("Walk", false);

        if (stage3.activeSelf && horizontalInput != 0)
        {
            stage3.GetComponentInChildren<Animator>().SetBool("Walk", true);

            if (horizontalInput < 0)
            {
                Quaternion targetRot = Quaternion.Euler(0, -90, 0);
                stage3.transform.rotation = Quaternion.RotateTowards(stage3.transform.rotation, targetRot, 10f);
            }
            else if (horizontalInput > 0)
            {
                Quaternion targetRot = Quaternion.Euler(0, 90, 0);
                stage3.transform.rotation = Quaternion.RotateTowards(stage3.transform.rotation, targetRot, 10f);
            }

        }
        else if (stage3.activeSelf)
            stage3.GetComponentInChildren<Animator>().SetBool("Walk", false);

        transform.Translate(Vector3.right * horizontalInput * movementSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if (stage2.activeSelf)
        {
            stage2.GetComponentInChildren<Animator>().SetBool("Land", false);
            stage2.GetComponentInChildren<Animator>().SetBool("Jump", true);
        }

        if (stage3.activeSelf)
        {
            stage3.GetComponentInChildren<Animator>().SetBool("Land", false);
            stage3.GetComponentInChildren<Animator>().SetBool("Jump", true);
        }

        grounded = false;
        upBoost = true;
        rigidbody.AddForce(Vector3.right * horizontalInput * jumpForce, ForceMode.Impulse);
        rigidbody.AddForce(Vector3.up * jumpForce * 2.5f, ForceMode.Impulse);
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
