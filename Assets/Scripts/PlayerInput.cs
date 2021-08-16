using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IInput
{
    public Action<Vector2> OnMovementInput { get; set; }
    public Action<Vector3> OnDirectionInput { get; set; }
    public Event OnJump { get; set; }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        GetMovementInput();
        GetMovementDirection();
        GetJump();
    }

    private void GetMovementDirection()
    {
        var cameraForwardDirection = Camera.main.transform.forward;
        var directionToMoveIn = Vector3.Scale(cameraForwardDirection, (Vector3.right + Vector3.forward));
        OnDirectionInput?.Invoke(directionToMoveIn);
    }

    private void GetMovementInput()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        OnMovementInput?.Invoke(input);
    }

    private void GetJump()
    {

    }
}
