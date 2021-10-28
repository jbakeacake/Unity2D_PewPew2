using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public abstract class EntityController : MonoBehaviour
{
    public Transform weaponOrigin;
    public List<ParticleSystemPair> particleSystems;
    public MovementOptions movementOptions;
    public float maxHealth;
    public float lookToSpeed;

    protected Rigidbody2D rb;
    protected Vector2 forward, lookDirection, knockbackVelocity;
    protected float xInput, yInput;
    protected float nextFire;
    protected float health;


    private bool isQuitting = false;
    
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
        ParticleSystem takeDamageParticleSystem = getParticleSystem("takeDamage");

        if (takeDamageParticleSystem.isPlaying) return;

        takeDamageParticleSystem.transform.up = projectileDirection.normalized;
        takeDamageParticleSystem.Play();
    }

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnDestroy()
    {
        if (isQuitting) return;
        instantiateParticleSystem("onDeath");
    }

    public ParticleSystem instantiateParticleSystem(String name)
    {
        ParticleSystem particleSystem = getParticleSystem(name);
        if (particleSystem != null)
        {
            GameObject clone = Instantiate(
                particleSystem,
                transform.position,
                transform.rotation
            ).gameObject;
            Destroy(clone, particleSystem.main.duration);
        }
        
        return particleSystem;
    }

    public ParticleSystem getParticleSystem(String name)
    {
        return this.particleSystems?.FirstOrDefault(psp => name.Equals(psp.name, StringComparison.OrdinalIgnoreCase))
            .particleSystem;
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