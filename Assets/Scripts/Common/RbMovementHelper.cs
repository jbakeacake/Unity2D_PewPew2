using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RbMovementHelper
{
    public static void applyMovement(Rigidbody2D rb, Vector2 forward, MovementOptions movementOptions)
    {
        rb.AddForce(forward * movementOptions.speed);
    }

    public static void addRecoil(Rigidbody2D rb, Vector2 projectileDirection, float knockBack)
    {
        rb.AddForce(projectileDirection * knockBack, ForceMode2D.Impulse);
    }
}
