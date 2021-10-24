using System;
using UnityEngine;

[Serializable]
public class ProjectileOptions
{
    public ParticleSystem impactParticleSystem;
    public float projectileDamage = 5.0f;
    public float speed = 20f;
    public float knockBack = 10f;
}
