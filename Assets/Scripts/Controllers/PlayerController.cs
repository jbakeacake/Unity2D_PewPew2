using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
    public Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextFire = 0f;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        setMovementDirection();
    }

    void FixedUpdate() {
        RbMovementHelper.applyMovement(rb, forward, movementOptions);
        TransformHelper.lookAtCursor(transform, playerCamera, lookToSpeed);
        TransformHelper.lookAtCursor(weaponOrigin, playerCamera, lookToSpeed);
        TransformHelper.swapSides(weaponOrigin, transform, playerCamera.ScreenToWorldPoint(Input.mousePosition));
    }

    public override void setMovementDirection()
    {
        forward = Vector2.zero;

        xInput = Mathf.Round(Input.GetAxis("Horizontal"));
        yInput = Mathf.Round(Input.GetAxis("Vertical"));

        Vector2 forwardStepDirection = yInput * Vector2.up;
        Vector2 sideStepDirection = xInput * Vector2.right;

        forward = (forwardStepDirection + sideStepDirection).normalized;
    }

    public override void useWeapon()
    {
        throw new System.NotImplementedException();
    }

}
