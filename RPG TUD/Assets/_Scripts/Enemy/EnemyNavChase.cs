using System.Collections;
using UnityEngine;

using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyNavChase : MonoBehaviour
{
    public Transform player;
    public Transform enemy;
    public PlayerStats playerHealth;
    public float baseSpeed = 3.5f;

    [Header("Attack")]
    public float attackRange = 1.5f;
    public float attackCooldown = 1.2f;
    public int attackDamage = 10;
    public float detectionRange = 10f;

    [Header("Animation")]
    public Animator animator;
    public float animationDampTime = 0.1f;

    private NavMeshAgent agent;
    private float nextAttackTime;
    private bool isAttacking;
    private bool isStunned;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    public void SetDifficulty(float speedMultiplier)
    {
        agent.speed = baseSpeed * speedMultiplier;
    }

    void Update()
    {
        if (isStunned || player == null || playerHealth == null)
            return;

        float distance = Vector3.Distance(enemy.transform.position, player.position);

        // Player too far away — idle
        if (distance > detectionRange)
        {
            Idle();
            return;
        }

        if (distance <= attackRange)
            TryAttack();
        else
            ChasePlayer();

        UpdateMovementAnimation();
    }

    void Idle()
    {
        agent.isStopped = true;
        animator.SetFloat("Speed", 0f);
    }

    void ChasePlayer()
    {
        isAttacking = false;

        if(animator)
            animator.SetBool("IsAttacking", false);

        agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    void TryAttack()
    {
        agent.isStopped = true;

        if(animator)
            animator.SetFloat("Speed", 0f);

        if (Time.time >= nextAttackTime)
        {
            animator.ResetTrigger("Attack");
            animator.SetTrigger("Attack");

            nextAttackTime = Time.time + attackCooldown;
        }

    }

    void UpdateMovementAnimation()
    {
        if (agent.isStopped) return;

        float speedPercent = agent.velocity.magnitude / agent.speed;

        if(animator)
            animator.SetFloat("speed", speedPercent, animationDampTime, Time.deltaTime);
    }


    public void DealDamage()
    {
        if (playerHealth == null) return;

        playerHealth.TakeDamage(attackDamage);
    }


    public void Stun(float duration)
    {
        if (isStunned) return;

        StartCoroutine(StunRoutine(duration));
    }

    IEnumerator StunRoutine(float duration)
    {
        isStunned = true;
        agent.isStopped = true;
        animator.SetFloat("Speed", 0f);
        animator.SetBool("IsAttacking", false);

        yield return new WaitForSeconds(duration);

        isStunned = false;
        agent.isStopped = false;
    }
}
