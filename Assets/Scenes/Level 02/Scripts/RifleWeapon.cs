using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(ParticleSystem), typeof(LineRenderer), typeof(AudioSource))]
[RequireComponent(typeof(Light))]
public class RifleWeapon : PlayerWeapon
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;//how far ray goes and can kill the enemy
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
        gunLine.SetPosition(0, transform.position);

        Ray shootRay = new Ray();
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            Debug.Log(shootHit.collider.GetComponent<NavMeshAgent>());
            if (enemyHealth != null)
            {
                Debug.Log("Health");
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }

    private bool IsWeaponReloaded()
    {
        return timer >= timeBetweenBullets;
    }
}

