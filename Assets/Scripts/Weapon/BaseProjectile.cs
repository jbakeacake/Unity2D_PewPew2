using System;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    [HideInInspector]
    public ProjectileOptions projectileOptions;

    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.rb.velocity = transform.up * projectileOptions.speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyController enemy = other.GetComponentInParent<EnemyController>();
        if (enemy != null)
        {
            enemy.takeDamage(this.rb.velocity.normalized, projectileOptions.projectileDamage, projectileOptions.knockBack);
        }

        GameObject impactClone = Instantiate(projectileOptions.impactParticleSystem, transform.position, transform.rotation).gameObject;
        impactClone.transform.up = -impactClone.transform.up;
        Destroy(impactClone, projectileOptions.impactParticleSystem.main.startLifetime.constant);
        Destroy(this.gameObject);
    }
}
