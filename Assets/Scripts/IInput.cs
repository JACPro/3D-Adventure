using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IInput
{
    Action<Vector2> OnMovementInput { get; set; }
    Action<Vector3> OnDirectionInput { get; set; }
    Event OnJump { get; set; }
}
