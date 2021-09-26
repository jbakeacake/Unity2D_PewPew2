using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityController : MonoBehaviour
{
    public Transform weaponOrigin;

    public MovementOptions movementOptions;

    protected Rigidbody2D rb;
    protected Vector2 forward, lookDirection, knockbackVelocity;
    protected float xInput, yInput;
    protected float nextFire;
    protected float health;

    public float maxHealth;

    public abstract void useWeapon();
    public abstract void setMovementDirection();

    public void takeDamage(Vector2 projectileDirection, float projectileDamage, float knockBack)
    {
        this.health -= projectileDamage;
        this.knockbackVelocity = projectileDirection * knockBack;
        if (this.health <= 0)
        {
            Debug.Log("DEATH!");
            Destroy(gameObject);
        }
    }

    protected void applyKnockback()
    {
        rb.AddForce(this.knockbackVelocity, ForceMode2D.Impulse);
        this.knockbackVelocity = Vector2.zero;
    }
}
