using UnityEngine;
using StarterAssets;
using Cinemachine;

public class CombatScript : MonoBehaviour
{
    [Header("Combat Settings")]
    public float attackCooldown = 0.75f;
    public float shootCooldown = .75f;
    public float castCooldown = 3f;
    public float health = 100f;
    private float lastAttackTime;
    private float lastShootTime;
    private float lastCastTime;
    public bool isShooting = false;
    public bool isCasting = false;
    public bool isMeleeing = false;
    
    [Header("Lock-on")]
    public float lockOnRadius = 20f;
    public LayerMask enemyMask;
    public Transform currentTarget;
    public bool isLockedOn = false;
    public float lockRotateSpeed = 10f;
    public float lockOnAngle = 60f;

    [Header("References")]
    public Animator animator;
    public Collider weaponCollider;
    private ThirdPersonController controller;
    public CinemachineVirtualCamera cinemachineCamera;
    public GameObject arrowPrefab;
    public GameObject spellPrefab;
    public GameObject shieldPrefab;
    public Transform spawnPoint;
    public GameObject player;
    public ParticleSystem swingParticles;
    public ParticleSystem hitParticles;
    public ParticleSystem shootParticles;
    public AudioClip shootSound;
    public AudioClip coinSound;
    public AudioClip oofSound;
    
    void Awake()
    {
        controller = GetComponent<ThirdPersonController>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (weaponCollider != null)
            weaponCollider.enabled = false;
    }

    void Update()
    {
        Cursor.visible = true;

        if (Input.GetMouseButtonDown(0))
        {
            TryShoot();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            TryAttack();
        }
        else if (Input.GetKey(KeyCode.F))
        {
            TryCast();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Roll");
        }
    }

    void TryAttack()
    {
        if (Time.time < lastAttackTime + attackCooldown && !isShooting && !isCasting)
            return;

        lastAttackTime = Time.time;
        isMeleeing = true;
        animator.SetTrigger("Attack");
    }

    void meleeParticles()
    {
        swingParticles.Play();
    }

    //search for "shoot" anim in files and check for animation event btw
    void shootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, spawnPoint.position, spawnPoint.rotation);
        //shootParticles.Play();

        /*if (currentTarget != null)
        {
            arrow.GetComponent<ProjectileScript>().SetTarget(currentTarget);
        }*/
    }

    //search for "cast" anim in files and check for animation event btw
    void castSpell()
    {
        GameObject spell = Instantiate(spellPrefab, spawnPoint.position, spawnPoint.rotation);
        //shootParticles.Play();

        if (currentTarget != null)
        {
            spell.GetComponent<ProjectileScript>().SetTarget(currentTarget);
        }
    }

    void TryShoot()
    {
        if (Time.time < lastShootTime + shootCooldown && !isMeleeing && !isCasting  )
            return;

        currentTarget = FindTargetOnShoot();
        lastShootTime = Time.time;
        isShooting = true;
        AudioSource.PlayClipAtPoint(shootSound, transform.position);
        animator.SetTrigger("Shoot");
    }

    void TryCast()
    {
        if (Time.time < lastCastTime + castCooldown && !isShooting && !isMeleeing)
            return;

        lastCastTime = Time.time;
        isCasting = true;
        animator.SetTrigger("Cast");
        AudioSource.PlayClipAtPoint(shootSound, transform.position);
    }


    //finnicky code but kinda works
    Transform FindTargetOnShoot()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, lockOnRadius, enemyMask);

        Transform bestTarget = null;
        float bestAngle = lockOnAngle;

        foreach (Collider hit in hits)
        {
            Vector3 dir = (hit.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, dir);

            if (angle < bestAngle)
            {
                bestAngle = angle;
                bestTarget = hit.transform;
            }
        }

        return bestTarget;
    }


}
