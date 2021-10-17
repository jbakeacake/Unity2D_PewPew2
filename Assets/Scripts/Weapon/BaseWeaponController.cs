using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class BaseWeaponController : MonoBehaviour
{
    [Header("General Options")] public Transform firePoint;
    public GameObject bulletPrefab;

    [Header("Weapon Options")] public float fireRate = 15f;
    [Range(0.1f, 1f)] public float deviationRampUp = 0.4f, deviationRampDown = 0.2f;
    [Range(1, 10)] public int bulletsPerFire = 1;
    public float firePointArc = 135.0f;
    public bool isAutomatic = true;


    private float nextFire = 0.0f;
    private float currentDeviation = 0.0f;
    

    void Update()
    {
        fire();
    }

    void fire()
    {
        if (canFire())
        {
            float firePointArcStep = firePointArc / bulletsPerFire;
            float currentAngle =
                (bulletsPerFire / 2) * -firePointArcStep; // start at the left most bullet in the spread

            for (int i = 0; i < bulletsPerFire; i += 2) // Instantiate each bullet, outside -> in
            {
                fireBullet(currentAngle);
                if (!Mathf.Approximately(currentAngle, 0.0f))
                {
                    fireBullet(-currentAngle);
                }
                currentAngle += firePointArcStep;
            }

            nextFire = Time.time + 1 / fireRate;
            firePoint.localRotation = Quaternion.identity;
        }
    }

    void fireBullet(float angleOfFire)
    {
        firePoint.localRotation = Quaternion.Euler(
            firePoint.localRotation.x,
            firePoint.localRotation.y,
            angleOfFire
        );
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Destroy(bullet, 10.0f);
    }

    bool canFire()
    {
        bool canFire = Time.time >= nextFire;
        if (isAutomatic)
        {
            canFire = canFire && Input.GetButton("Fire1");
        }
        else
        {
            canFire = canFire && Input.GetButtonDown("Fire1");
        }


        return canFire;
    }
}