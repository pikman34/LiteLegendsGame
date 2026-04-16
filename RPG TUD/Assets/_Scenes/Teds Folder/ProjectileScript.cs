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

            // Smooth rotation
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
}
