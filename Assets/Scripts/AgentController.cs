using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    IInput input;
    AgentMovement movement; 

    void Awake()
    {
        input = GetComponent<IInput>();
        movement = GetComponent<AgentMovement>();
    }

    void OnEnable() {
        input.OnDirectionInput += movement.HandleMovementDirection;
        input.OnMovementInput += movement.HandleMovement;
    }

    private void OnDisable() {
        input.OnDirectionInput -= movement.HandleMovementDirection;
        input.OnMovementInput -= movement.HandleMovement;
    }
}
