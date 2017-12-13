using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(ParticleSystem), typeof(LineRenderer), typeof(AudioSource))]
[RequireComponent(typeof(Light))]
public class ShotgunWeapon : PlayerWeapon
{
    public int damagePerShot = 10;
    public int pelletsCount = 19;
    public float fireConeAngle = 45.0f;
    public float timeBetweenBullets = 0.5f;
    public float range = 5f;
    public float effectsDisplayTime = 0.2f;

    float timer;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;


    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();

        gunLine = GetComponent<LineRenderer>();
        gunLine.positionCount = pelletsCount * 2;

        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        gunLine.SetPositions(new Vector3[] {}); // clear positions
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    public override bool TryShoot()
    {
        if (!IsWeaponReloaded())
        {
            return false;
        }

        Shoot();
        return true;
    }

    private void Shoot()
    {
        timer = 0f;

        gunAudio.Play();

        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        var gunLinePositions = new List<Vector3>();

        var shootRays = BuildRays();

        for (int i = 0; i < shootRays.Count(); ++i)
        {
            var shootRay = shootRays[i];

            gunLinePositions.Add(transform.position);

            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            {
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damagePerShot, shootHit.point);
                }
               gunLinePositions.Add(shootHit.point);
            }
            else
            {
                gunLinePositions.Add(shootRay.origin + shootRay.direction * range);
            }
        }

        gunLine.SetPositions(gunLinePositions.ToArray());
    }

    private bool IsWeaponReloaded()
    {
        return timer >= timeBetweenBullets;
    }

    private List<Ray> BuildRays()
    {
        var rays = new List<Ray>(pelletsCount);

        for (int i = 0; i < pelletsCount; ++i)
        {
            Ray currentRay = new Ray();

            currentRay.origin = transform.position;

            float t = i / ((float) (pelletsCount - 1));
            float rotationAngle = Mathf.Lerp(-fireConeAngle / 2.0f, fireConeAngle / 2.0f, t);
            var rotation = Quaternion.Euler(0.0f, rotationAngle, 0.0f);
            currentRay.direction = (rotation * transform.forward).normalized;

            rays.Add(currentRay);
        }

        return rays;
    }
}

