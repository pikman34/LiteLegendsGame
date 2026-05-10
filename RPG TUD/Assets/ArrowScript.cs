using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ArrowProjectile : MonoBehaviour
{
    public float speed = 15f;
    public float lifeTime = 5f;
    public float damage = 10f;
    public AudioClip arrowSound;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rb.linearVelocity = transform.forward * speed;
        AudioSource.PlayClipAtPoint(arrowSound, transform.position);
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            return;

        Destroy(gameObject);
    }
}