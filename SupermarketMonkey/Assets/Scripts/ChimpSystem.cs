using UnityEngine;
using UnityEngine.UI;

public class ChimpSystem : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBarFill;

    public GameObject waypoints;
    private int target = 0;

    public float moveSpeed = 5f;
    private float baseSpeed;

    public float rotationSpeed = 10f;
    private float fixedY;

    private bool isDead = false;
    public Vector3 velocity;
    private Vector3 lastPosition;

    public ChimpSpawner spawner;

    void Start()
    {
        currentHealth = maxHealth;
        spawner = ChimpSpawner.Instance;

        fixedY = transform.position.y;
        baseSpeed = moveSpeed;

        // initialize velocity tracking
        lastPosition = transform.position;
        velocity = Vector3.zero;
    }

    void Update()
    {
        if (currentHealth <= 0 && !isDead)
{
    Die();
    return;
}
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;

        

        Transform targetWaypoint = waypoints.transform.GetChild(target);

        // Keep movement on fixed Y plane
        Vector3 targetPosition = targetWaypoint.position;
        targetPosition.y = fixedY;

        // Movement direction
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        // Move along waypoints
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        // Rotate toward movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // Waypoint switching
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            target++;

            if (target >= waypoints.transform.childCount)
            {
                target = 0;
            }
        }

        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        float healthPercent = currentHealth / maxHealth;
        healthBarFill.fillAmount = healthPercent;
    }

    void Die()
{
    if (isDead)
        return;

    isDead = true;

    if (spawner != null)
    {
        spawner.OnChimpDeath();
    }

    Destroy(gameObject);
}
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Slow"))
        {
            moveSpeed = other.GetComponent<SlowTower>().slowrate;
        }
        if (other.CompareTag("Base"))
        {
            other.GetComponent<Base>().TakeDamage(1);
            currentHealth = 0;
            Die();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Slow"))
        {
            moveSpeed = baseSpeed;
        }
    }
}