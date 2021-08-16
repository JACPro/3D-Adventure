using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    CharacterController controller;
    Animator animator;
    [SerializeField] float rotationSpeed, movementSpeed, gravity = 20f, jumpForce = 5f;
    Vector3 movementVector  = Vector3.zero;
    float desiredRotationAngle = 0;

    void Start()
    {
        controller = GetComponent<CharacterController>();    
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (controller.isGrounded)
        {
            if (movementVector.magnitude > 0)
            {
                var animationSpeedMultiplier = SetCorrectAnimation();
                RotateAgent();
                movementVector *= animationSpeedMultiplier;
            }
        }
        movementVector.y -= gravity;
        controller.Move(movementVector * Time.deltaTime);    
    }

    public void HandleMovement(Vector2 input)
    {
        if (controller.isGrounded)
        {
            if (input.y > 0)
            {
                movementVector = transform.forward*movementSpeed;
            }
            else
            {
                movementVector = Vector3.zero;
                animator.SetFloat("Move", 0);
            }
        }
    }

    public void HandleMovementDirection(Vector3 direction)
    {
        desiredRotationAngle = Vector3.Angle(transform.forward, direction);
        var crossProduct = Vector3.Cross(transform.forward, direction).y;
        if (crossProduct < 0)
        {
            desiredRotationAngle *= -1;
        }
    }

    public void HandleJump(float jumpForce)
    {

    }

    void RotateAgent()
    {
        //only rotate past a certain threshold
        if (desiredRotationAngle > 10 || desiredRotationAngle < -10)
        {
            transform.Rotate(Vector3.up * desiredRotationAngle * rotationSpeed * Time.deltaTime);
        }
    }

    float SetCorrectAnimation()
    {
        float currentAnimationSpeed = Mathf.Max(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (desiredRotationAngle > 10 || desiredRotationAngle < -10 )
        {
            //if still or walking
            if (currentAnimationSpeed < 0.2f)
            {
                currentAnimationSpeed += Time.deltaTime * 2;
                currentAnimationSpeed = Mathf.Clamp(currentAnimationSpeed, 0, 0.2f);
            }
        }
        else
        {
            if (currentAnimationSpeed < 1)
            {
                currentAnimationSpeed += Time.deltaTime * 2;
            }
            else
            {
                currentAnimationSpeed = 1;
            }
        }
        animator.SetFloat("Move", currentAnimationSpeed);
        return currentAnimationSpeed;
    }
}
