using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    public ParticleSystem impactParticleSystem;
    public float projectileDamage = 5.0f;
    public float speed = 20f;
    public float knockBack = 10f;
    public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        this.rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyController enemy = other.GetComponentInParent<EnemyController>();
        if (enemy != null)
        {
            enemy.takeDamage(this.rb.velocity.normalized, projectileDamage, knockBack);
        }

        GameObject impactClone = Instantiate(impactParticleSystem, transform.position, transform.rotation).gameObject;
        impactClone.transform.up = -impactClone.transform.up;
        Destroy(impactClone, impactParticleSystem.main.startLifetime.constant);
        Destroy(this.gameObject);
    }
}
