using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;

    void Update()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        transform.LookAt(player);
        transform.position += direction * speed * Time.deltaTime;
    }
}
