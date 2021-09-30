using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RbMovementHelper
{
    public static void applyMovement(Rigidbody2D rb, Vector2 forward, MovementOptions movementOptions)
    {
        Debug.Log(movementOptions.speed * forward);
        rb.AddForce(forward * movementOptions.speed);
    }

    public static void applyCounterMovement(Rigidbody2D rb, Vector2 forward, float xInput, float yInput, MovementOptions movementOptions)
    {
        Vector2 finalVelocity = Vector3.zero;
        if (Math.Abs(forward.x) > movementOptions.counterMovementThreshold && Math.Abs(xInput) < 0.05f)
        {
            finalVelocity += movementOptions.speed * forward.normalized * Time.deltaTime * -forward.x *
                             movementOptions.counterMovementMagnitude;
        }

        if (Math.Abs(forward.y) > movementOptions.counterMovementThreshold && Math.Abs(yInput) < 0.05f)
        {
            finalVelocity += movementOptions.speed * forward.normalized * Time.deltaTime * -forward.y *
                             movementOptions.counterMovementMagnitude;
        }

        rb.AddForce(finalVelocity);
    }
}
