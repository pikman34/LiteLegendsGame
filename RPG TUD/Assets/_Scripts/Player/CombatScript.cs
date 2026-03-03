using UnityEngine;
using StarterAssets;
using System;
using Cinemachine;

public class CombatScript : MonoBehaviour
{
    [Header("Combat Settings")]
    public float attackCooldown = 0.8f;
    public float shootCooldown = 1f;
    public float castCooldown = 3f;
    public int attackDamage = 25;
    public int shootDamage = 15;
    public bool isShooting = false;
    public bool isCasting = false;
    public bool isMeleeing = false;
    
    [Header("Lock-on")]
    public float lockRange = 20f;
    public Transform currentTarget;
    public float rotateSpeed = 25f;
    public bool isLocked = false;

    [Header("References")]
    public Animator animator;
    public Collider weaponCollider;
    private float lastAttackTime;
    private float lastShootTime;
    private float lastCastTime;

    private ThirdPersonController controller;
    public CinemachineVirtualCamera cinemachineCamera;

    public ParticleSystem swingParticles;
    public ParticleSystem hitParticles;
    public ParticleSystem shootParticles;
    public GameObject player;

    

    void Awake()
    {
        controller = GetComponent<ThirdPersonController>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (weaponCollider != null)
            weaponCollider.enabled = false;
    }

    void Update()
    {
        //FindNearestEnemy();

        /*if (isLocked && currentTarget != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);
            if (distanceToTarget > lockRange)
            {
                UnlockTarget();
            }
        }*/


        if (Input.GetMouseButtonDown(0))
        {
            TryShoot();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            TryAttack();
        }
        else if (Input.GetKey(KeyCode.F))
        {
            TryCast();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetTrigger("Roll");
        }
    }

    void TryAttack()
    {
        if (Time.time < lastAttackTime + attackCooldown && !isShooting && !isCasting)
            return;

        lastAttackTime = Time.time;
        isMeleeing = true;
        animator.SetTrigger("Attack");
        
    }

    void meleeParticles()
    {
        swingParticles.Play();
    }

    void TryShoot()
    {
        if (Time.time < lastShootTime + shootCooldown && !isMeleeing && !isCasting  )
            return;

        lastShootTime = Time.time;

        isShooting = true;
        animator.SetTrigger("Shoot");
        
    }

    void TryCast()
    {
        if (Time.time < lastCastTime + castCooldown && !isShooting && !isMeleeing)
            return;

        lastCastTime = Time.time;

        isCasting = true;
        animator.SetTrigger("Cast");
        
    }

    void ToggleSoftLock()
    {
        if (currentTarget == null)
            return;

        isLocked = !isLocked;

        if (isLocked)
            cinemachineCamera.LookAt = currentTarget;
        else
            cinemachineCamera.LookAt = player.transform; // Reset camera to player
    }

    void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform nearest = null;
        float shortestDistance = lockRange;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= shortestDistance)
            {
                shortestDistance = distance;
                nearest = enemy.transform;
            }
        }

        currentTarget = nearest; // null if none in range
    }

    void UnlockTarget()
    {
        isLocked = false;
        currentTarget = null;
    }
   
}
