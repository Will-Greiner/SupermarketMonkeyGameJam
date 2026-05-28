using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleShotTower : MonoBehaviour
{
    public List<Transform> enemiesInRange = new List<Transform>();
    public Transform currentTarget;

    public GameObject projectilePrefab;
    public float fireRate = 1f;
    public float damage = 25f;
    public Transform firePoint;
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
            Shoot();
            fireCooldown = 1f / fireRate;
        }
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
        if (other.CompareTag("Chimp"))   // FIXED
        {
            enemiesInRange.Remove(other.transform);
        }
    }

    void Shoot()
    {
        if (currentTarget == null) return;

        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        Projectile p = proj.GetComponent<Projectile>();
        if (p != null)
        {
            p.SetTarget(currentTarget, damage);
        }

        Debug.Log("Shot fired at: " + currentTarget.name);
    }
}