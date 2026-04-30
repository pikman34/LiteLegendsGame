using System.Collections;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    public float lifetime;
    private Transform target;
    private float timer;
    public float isSpell;
    public float isArrow;
    public float damage;
    public GameObject hitEffect;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Collider[] playerColliders = player.GetComponentsInChildren<Collider>();
        Collider myCollider = GetComponent<Collider>();

        foreach (Collider col in playerColliders)
        {
            Physics.IgnoreCollision(myCollider, col);
        }
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Destroy(gameObject);
            return;
        }

        if (isSpell == 1)
        {
            if (target == null)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                return;
            }

            Vector3 dir = (target.position - transform.position).normalized;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, rotateSpeed * Time.deltaTime);
        }
        if (isArrow == 1)
        {
            //123
        }
        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player")) return;

        if (hitEffect != null)
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);   
        }

        Destroy(gameObject);
    }
}
