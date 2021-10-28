using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TreeEditor;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : EntityController
{
    public LayerMask obstacleLayer;

    private Transform player;
    private NavMeshAgent agent;
    private float originalStoppingDistance;
    private bool isActive;
    private void Awake()
    {
        StartCoroutine(performSpawnInAnimation());
    }

    // Start is called before the first frame update
    void Start()
    {
        this.health = this.maxHealth;
        this.player = FindObjectOfType<PlayerController>().gameObject.transform;
        this.agent = GetComponent<NavMeshAgent>();
        this.rb = GetComponent<Rigidbody2D>();
        this.originalStoppingDistance = this.agent.stoppingDistance;

        this.agent.updatePosition = false; // Boo to transforms, Yay to physics
        this.agent.updateRotation = false;
        this.agent.updateUpAxis = false;
    }

    void Update()
    {
        if (!isActive) return;
        TransformHelper.lookAtTarget(this.weaponOrigin.transform, player);
        TransformHelper.lookAtTarget(this.transform, player);
        TransformHelper.swapSides(weaponOrigin, transform, player.position);
        this.setMovementDirection();
        this.agent.nextPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (!isActive) return;
        RbMovementHelper.applyMovement(this.rb, this.forward, this.movementOptions);
    }

    public override void useWeapon()
    {
        throw new System.NotImplementedException();
    }

    public override void setMovementDirection()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, 30.0f,
            this.obstacleLayer);
        if (hit && !hit.collider.gameObject.CompareTag("Player"))
        {
            chasePlayer();
        }
        else
        {
            Vector2 distanceFromPlayer = transform.position - player.position; // Vector2.Distance(transform.position, player.position);
            if (distanceFromPlayer.sqrMagnitude <= this.originalStoppingDistance * this.originalStoppingDistance)
            {
                backAwayFromPlayer();
            }
            else
            {
                maintainStoppingDistanceFromPlayer();
            }
        }
        
        this.forward = this.agent.desiredVelocity.normalized;
    }

    private IEnumerator performSpawnInAnimation()
    {
        List<SpriteRenderer> spriteRenderers = GetComponentsInChildren<SpriteRenderer>().ToList();
        List<Collider2D> collider2Ds = GetComponentsInChildren<Collider2D>().ToList();
        this.isActive = false;
        spriteRenderers.ForEach(sr => sr.enabled = false);
        collider2Ds.ForEach(col => col.enabled = false);
        
        ParticleSystem particleSystem = instantiateParticleSystem("onSpawn");
        
        yield return new WaitForSeconds(particleSystem?.main.duration ?? 0.0f);

        this.isActive = true;
        spriteRenderers.ForEach(sr => sr.enabled = true);
        collider2Ds.ForEach(col => col.enabled = true);
    }

    private void chasePlayer()
    {
        this.agent.stoppingDistance = 1.5f; // Get as close as we can to the player
        this.agent.SetDestination(player.position);
    }

    private void backAwayFromPlayer()
    {
        this.agent.FindClosestEdge(out var closestEdge);
        this.agent.stoppingDistance = 1.0f;
        this.agent.SetDestination(closestEdge.position);
    }

    private void maintainStoppingDistanceFromPlayer()
    {
        this.agent.stoppingDistance = this.originalStoppingDistance;
        this.agent.SetDestination(player.position);
    }
}