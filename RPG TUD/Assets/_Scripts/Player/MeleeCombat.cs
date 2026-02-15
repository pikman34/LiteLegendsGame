using UnityEngine;
using StarterAssets;

public class MeleeCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public float attackCooldown = 0.8f;
    public int attackDamage = 25;

    [Header("References")]
    public Animator animator;
    public Collider weaponCollider;

    private float lastAttackTime;
    private ThirdPersonController controller;

    public ParticleSystem swingParticles;

    void Awake()
    {
        controller = GetComponent<ThirdPersonController>();

        if (weaponCollider != null)
            weaponCollider.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryAttack();
        }
    }

    void TryAttack()
    {
        if (Time.time < lastAttackTime + attackCooldown)
            return;

        lastAttackTime = Time.time;

        animator.SetTrigger("Attack");
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

    public void MeleeSwingEffect()
    {
        swingParticles.Play();
    }
}
