using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BurstTower : MonoBehaviour
{
    public List<Transform> enemiesInRange = new List<Transform>();
    public Transform currentTarget;

    public GameObject projectilePrefab;
    public float damage = 25f;
    public Transform firePoint;

    [Header("Burst Settings")]
    public int burstCount = 5;
    public float spreadAngle = 30f;

    [Header("Fire Rate")]
    public float fireRate = 1f;
    private float fireCooldown = 0f;

    public GameObject emptySpace;
    public float damageIncrease;
    public float rangeIncrease;
    public float fireRateDecrease;
    public int tier = 1;
    public Button upgradeButton;
    public Button deleteButton;
    public Transform cameraRef;
    void Start()
    {
        upgradeButton.onClick.AddListener(UpgradeListener);
        deleteButton.onClick.AddListener(DeleteListener);
    }
    void UpgradeListener()
    {
        if(tier < 3)
        {
            tier++;
            fireRate = fireRate - fireRateDecrease;
            //setup range changer
            damage = damage + damageIncrease;
        }
    }
    void DeleteListener()
    {
        Instantiate(emptySpace, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void Update()
    {
        CleanNullTargets();
        SelectTarget();

        fireCooldown -= Time.deltaTime;

        if (currentTarget != null && fireCooldown <= 0f)
        {
            ShootBurst();
            fireCooldown = 1f / fireRate;
        }
    }

    void ShootBurst()
    {
        if (currentTarget == null) return;

        Vector3 baseDirection = (currentTarget.position - firePoint.position).normalized;

        for (int i = 0; i < burstCount; i++)
        {
            float angleOffset = Random.Range(-spreadAngle, spreadAngle);
            Quaternion spreadRotation = Quaternion.Euler(0, angleOffset, 0);

            Vector3 finalDirection = spreadRotation * baseDirection;

            GameObject proj = Instantiate(
                projectilePrefab,
                firePoint.position,
                Quaternion.LookRotation(finalDirection)
            );

            Projectile p = proj.GetComponent<Projectile>();

            if (p != null)
            {
                p.SetDirection(finalDirection, damage);
            }
        }

        Debug.Log("Burst fired at once: " + currentTarget.name);
    }

    void SelectTarget()
    {
        Transform closestEnemy = null;
        float closestDist = Mathf.Infinity;

        foreach (var enemy in enemiesInRange)
        {
            float dist = Vector3.Distance(transform.position, enemy.position);

            if (dist < closestDist)
            {
                closestDist = dist;
                closestEnemy = enemy;
            }
        }

        currentTarget = closestEnemy;
    }

    void CleanNullTargets()
    {
        enemiesInRange.RemoveAll(enemy => enemy == null);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chimp"))
        {
            enemiesInRange.Add(other.transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Chimp"))
        {
            enemiesInRange.Remove(other.transform);
        }
    }
}