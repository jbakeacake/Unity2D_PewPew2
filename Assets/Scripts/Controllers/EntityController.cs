using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class EntityController : MonoBehaviour
{
    public Transform weaponOrigin;
    public List<ParticleSystemPair> particleSystems;
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
        this.applyKnockback(projectileDirection, knockBack);
        this.playTakeDamageParticleSystem(-projectileDirection);
        if (this.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void playTakeDamageParticleSystem(Vector2 projectileDirection)
    {
        ParticleSystem takeDamageParticleSystem = this.particleSystems
            .First(psp => psp.name.Equals("takeDamage", StringComparison.OrdinalIgnoreCase))
            .particleSystem;

        if (takeDamageParticleSystem.isPlaying) return;

        takeDamageParticleSystem.transform.up = projectileDirection.normalized;
        takeDamageParticleSystem.Play();
    }

    protected void applyKnockback(Vector2 projectileDirection, float knockBack)
    {
        RbMovementHelper.addRecoil(this.rb, projectileDirection, knockBack);
    }

    [Serializable]
    public struct ParticleSystemPair
    {
        public string name;
        public ParticleSystem particleSystem;
    }
}