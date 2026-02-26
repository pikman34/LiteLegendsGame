using UnityEngine;
using StarterAssets;

public class CombatScript : MonoBehaviour
{
    [Header("Combat Settings")]
    public float attackCooldown = 0.8f;
    public float shootCooldown = 1f;
    public float castCooldown = 3f;
    public int attackDamage = 25;
    public int shootDamage = 15;
    

    [Header("References")]
    public Animator animator;
    public Collider weaponCollider;
    private float lastAttackTime;
    private float lastShootTime;
    private float lastCastTime;

    private ThirdPersonController controller;

    public ParticleSystem swingParticles;
    public ParticleSystem hitParticles;
    public ParticleSystem shootParticles;

    void Awake()
    {
        controller = GetComponent<ThirdPersonController>();

        if (weaponCollider != null)
            weaponCollider.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click for shooting
        {
            TryShoot();
        }
        else if (Input.GetMouseButtonDown(1)) // Right-click for melee attack
        {
            TryAttack();
        }
        else if (Input.GetKey(KeyCode.F)) // F key for magic
        {
            TryCast();
        }
    }

    void TryAttack()
    {
        if (Time.time < lastAttackTime + attackCooldown)
            return;

        lastAttackTime = Time.time;

        animator.SetTrigger("Attack");
        swingParticles.Play();
    }

    void TryShoot()
    {
        if (Time.time < lastShootTime + shootCooldown)
            return;

        lastShootTime = Time.time;

        animator.SetTrigger("Shoot");
    }

    void TryCast()
    {
        if (Time.time < lastCastTime + castCooldown)
            return;

        lastCastTime = Time.time;

        animator.SetTrigger("Cast");
    }

    // Called by animation event
    public void EnableWeapon()
    {
        if (weaponCollider != null)
            weaponCollider.enabled = true;
    }

    // Called by animation event
    public void DisableWeapon()
    {
        if (weaponCollider != null)
            weaponCollider.enabled = false;
    }

}
