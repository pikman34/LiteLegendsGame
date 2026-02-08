using UnityEngine;


public class MeleeHitBox : MonoBehaviour
{
    public int damage = 25;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
