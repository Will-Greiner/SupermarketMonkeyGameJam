using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 10f;

    private Vector3 direction;

    private Transform target;
    private bool hasTarget = false;

    public float damage;
    public float lifetime = 1.5f;
    private float timer;
    // =========================
    // OLD SYSTEM (target-based)
    // =========================

    void Start()
    {
        timer = lifetime;
    }
    public void SetTarget(Transform newTarget, float dmg)
    {
        target = newTarget;
        damage = dmg;
        hasTarget = true;
    }

    // =========================
    // NEW SYSTEM (direction-based)
    // =========================
    public void SetDirection(Vector3 dir, float dmg)
    {
        direction = dir.normalized;
        damage = dmg;
        hasTarget = false;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Destroy(gameObject);
            return;
        }
        if (hasTarget)
        {
            MoveTowardsTarget();
        }
        else
        {
            MoveInDirection();
        }
    }

    // =========================
    // OLD BEHAVIOR (tracking)
    // =========================
    void MoveTowardsTarget()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = (target.position - transform.position).normalized;

        transform.position += dir * speed * Time.deltaTime;
        transform.forward = dir;
    }

    // =========================
    // NEW BEHAVIOR (straight / arc systems)
    // =========================
    void MoveInDirection()
    {
        transform.position += direction * speed * Time.deltaTime;
        transform.forward = direction;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chimp"))
        {
            ChimpSystem hp = other.GetComponent<ChimpSystem>();

            if (hp != null)
            {
                hp.currentHealth -= damage;
            }

            Destroy(gameObject);
        }
    }
}