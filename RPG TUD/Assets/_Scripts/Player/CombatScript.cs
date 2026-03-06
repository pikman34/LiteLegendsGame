using UnityEngine;
using StarterAssets;
using System;
using Cinemachine;
using UnityEngine.InputSystem;

public class CombatScript : MonoBehaviour
{
    [Header("Combat Settings")]
    public float attackCooldown = 0.75f;
    public float shootCooldown = .75f;
    public float castCooldown = 3f;
    private float lastAttackTime;
    private float lastShootTime;
    private float lastCastTime;
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

    private ThirdPersonController controller;
    public CinemachineVirtualCamera cinemachineCamera;
    public GameObject arrowPrefab;
    public Transform spawnPoint;

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
        Cursor.visible = true;

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
        if (Input.GetKeyDown(KeyCode.Space))
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

    void shootArrow()
    {
        Instantiate(arrowPrefab, spawnPoint.position, spawnPoint.rotation);
        //shootParticles.Play();
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
}
