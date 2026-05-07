using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleEnemy : MonoBehaviour
{
    public Transform player;
    public Transform visual;

    public float detectionRadius = 8f;
    public float loseRadius = 12f;

    public float moveSpeed = 2f;
    public float orbitSpeed = 3f;
    public float attackSpeed = 8f;

    public float orbitTime = 1.5f;
    public float damage = 10f;
    public float health = 50f;

    public float patrolRadius = 4f;
    public float patrolWaitTime = 2f;
    public float patrolPointReachDistance = 0.4f;
    public float patrolMinPointDistance = 1.0f;

    public float wobbleAmount = 25f;
    public float wobbleSpeed = 8f;
    public float visualTurnSpeed = 12f;
    public float modelYawOffset = 0f;

    private Rigidbody rb;

    private Vector3 spawnPoint;
    private Vector3 patrolTarget;
    private float patrolTimer;

    private float orbitTimer;
    private Vector3 attackDirection;

    private float wobbleTimer;
    private Vector3 currentMoveDir = Vector3.forward;

    private enum State { Patrol, Chase, Orbit, Attack }
    private State state = State.Patrol;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spawnPoint = rb.position;
        PickNewPatrolPoint();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distToPlayer = FlatDistance(rb.position, player.position);
        bool isMoving = false;

        switch (state)
        {
            case State.Patrol:
                if (distToPlayer < detectionRadius)
                {
                    state = State.Chase;
                    break;
                }

                isMoving = PatrolBehaviour();
                break;

            case State.Chase:
                if (distToPlayer > loseRadius)
                {
                    state = State.Patrol;
                    PickNewPatrolPoint();
                    break;
                }

                MoveTowards(player.position, moveSpeed);
                isMoving = true;

                if (distToPlayer < 2f)
                {
                    state = State.Orbit;
                    orbitTimer = orbitTime;
                }
                break;

            case State.Orbit:
                if (distToPlayer > loseRadius)
                {
                    state = State.Patrol;
                    PickNewPatrolPoint();
                    break;
                }

                orbitTimer -= Time.fixedDeltaTime;

                Vector3 dir = FlatDirection(rb.position, player.position);
                Vector3 perp = new Vector3(-dir.z, 0f, dir.x);

                if (dir.sqrMagnitude > 0.0001f)
                    currentMoveDir = dir;

                Vector3 orbitMove = (perp.normalized * 0.8f + dir * 0.2f) * orbitSpeed * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + orbitMove);

                isMoving = true;

                if (orbitTimer <= 0f)
                {
                    state = State.Attack;
                    attackDirection = FlatDirection(rb.position, player.position);
                }
                break;

            case State.Attack:
                if (attackDirection.sqrMagnitude > 0.0001f)
                    currentMoveDir = attackDirection;

                Vector3 attackMove = attackDirection * attackSpeed * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + attackMove);

                isMoving = true;

                if (distToPlayer > 3f)
                    state = State.Chase;
                break;
        }

        HandleVisual(isMoving);
    }

    bool PatrolBehaviour()
    {
        float distToPoint = FlatDistance(rb.position, patrolTarget);

        if (distToPoint > patrolPointReachDistance)
        {
            MoveTowards(patrolTarget, moveSpeed * 0.6f);
            return true;
        }

        patrolTimer -= Time.fixedDeltaTime;

        if (patrolTimer <= 0f)
            PickNewPatrolPoint();

        return false;
    }

    void PickNewPatrolPoint()
    {
        Vector3 newPoint = spawnPoint;
        int tries = 0;

        do
        {
            Vector2 random = Random.insideUnitCircle * patrolRadius;
            newPoint = spawnPoint + new Vector3(random.x, 0f, random.y);
            tries++;
        }
        while (FlatDistance(spawnPoint, newPoint) < patrolMinPointDistance && tries < 12);

        patrolTarget = newPoint;
        patrolTimer = patrolWaitTime;
    }

    void MoveTowards(Vector3 target, float speed)
    {
        Vector3 dir = FlatDirection(rb.position, target);

        if (dir.sqrMagnitude <= 0.0001f)
            return;

        currentMoveDir = dir;

        Vector3 movement = dir * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }

    Vector3 FlatDirection(Vector3 from, Vector3 to)
    {
        Vector3 dir = to - from;
        dir.y = 0f;

        if (dir.sqrMagnitude <= 0.0001f)
            return Vector3.zero;

        return dir.normalized;
    }

    float FlatDistance(Vector3 a, Vector3 b)
    {
        Vector3 diff = b - a;
        diff.y = 0f;
        return diff.magnitude;
    }

    void HandleVisual(bool moving)
    {
        if (visual == null) return;
        if (currentMoveDir.sqrMagnitude <= 0.0001f) return;

        Quaternion faceRot = Quaternion.LookRotation(currentMoveDir, Vector3.up) * Quaternion.Euler(0f, modelYawOffset, 0f);

        if (moving)
        {
            wobbleTimer += Time.fixedDeltaTime;
            float angle = Mathf.Sin(wobbleTimer * wobbleSpeed) * wobbleAmount;

            Quaternion wobbleRot = faceRot * Quaternion.Euler(0f, 0f, angle);

            visual.rotation = Quaternion.Slerp(
                visual.rotation,
                wobbleRot,
                visualTurnSpeed * Time.fixedDeltaTime
            );
        }
        else
        {
            wobbleTimer = 0f;

            visual.rotation = Quaternion.Slerp(
                visual.rotation,
                faceRot,
                visualTurnSpeed * Time.fixedDeltaTime
            );
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
            Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            ProjectileScript proj = other.GetComponent<ProjectileScript>();

            if (proj != null)
            {
                TakeDamage(proj.damage);
            }
        }
    }

    public void Die()
    {
        //AAAAA
        Destroy(gameObject);
    }
}