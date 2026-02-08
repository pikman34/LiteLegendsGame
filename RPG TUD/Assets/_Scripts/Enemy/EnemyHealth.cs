using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public int attackDamage = 5;
    public int xpReward = 20;
    public float hitStunDuration = 0.4f;
    public Animator animator;
    public EnemyNavChase enemyAI;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        

        Debug.Log("Enemy damaged! , health is : " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            PlayHitReaction();
        }
    }

    void PlayHitReaction()
    {
        animator.SetTrigger("IsHit");

        if (enemyAI != null)
            enemyAI.Stun(hitStunDuration);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
