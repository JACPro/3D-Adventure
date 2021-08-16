using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float strafeSpeed = 4f;
    [SerializeField] float jumpForce = 3f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float rotationSpeed = 5f;
    Camera cam;
    Vector2 moveInput;
    Vector3 playerForward;
    Vector3 movementVector;
    float rotationAngle;
    CharacterController controller;
    Animator animator;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        movementVector = Vector3.zero;
        cam = Camera.main;
    }

    private void Update()
    {
        HandleInput();
        if (controller.isGrounded)
        {
            HandleRotation();
            CalculateMoveVector();
        }
        HandleMoveDirection();
        HandleJump();
        HandleMovement();
        HandleAnimation();
    }
    private void HandleAnimation()
    {
        float animSpeed = Mathf.Max(Mathf.Abs(moveInput.x), Mathf.Abs(moveInput.y));
        animator.SetFloat("Move", animSpeed);
    }

    private void CalculateMoveVector()
    {
        Vector3 forwardMovement = Vector3.zero;
        Vector3 sidewaysMovement = Vector3.zero;

        if (Mathf.Abs(moveInput.y) > 0)
        {
            forwardMovement = transform.forward * moveInput.y * walkSpeed;
        }
        else if (Mathf.Abs(moveInput.x) > 0)
        {
            sidewaysMovement = transform.right * moveInput.x * strafeSpeed;
        }
        
        movementVector = forwardMovement + sidewaysMovement;
        Debug.Log(movementVector);
    }

    private void RotatePlayer()
    {
        if (rotationAngle > 10 || rotationAngle < -10)
        {
            transform.Rotate(Vector3.up * rotationAngle * rotationSpeed * Time.deltaTime);
        }
    }

    private void HandleRotation()
    {
        if (movementVector.magnitude > 0)
        {
            RotatePlayer();
        }
    }

    private void HandleMoveDirection()
    {
        playerForward = Vector3.Scale(cam.transform.forward, Vector3.forward + Vector3.right);
        rotationAngle = Vector3.Angle(transform.forward, playerForward);
        if (Vector3.Cross(transform.forward, playerForward).y < 0)
        {
            rotationAngle *= -1;
        }
    }

    private void HandleJump()
    {
        Debug.Log(controller.isGrounded + " ");
        if (controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("Jumped");
                movementVector.y += (jumpForce * -1.0f * gravity);
            }
            else if (movementVector.y < 0)
            {
                movementVector.y = 0;
            }
        }
        else
        {
            movementVector.y += gravity;
        }
    }

    private void HandleMovement()
    {
        controller.Move(movementVector * Time.deltaTime);
    }

    private void HandleInput()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}