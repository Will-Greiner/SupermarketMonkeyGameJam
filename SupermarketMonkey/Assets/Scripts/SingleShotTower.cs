using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleShotTower : MonoBehaviour
{
    
    public GameObject tier1;
    public GameObject tier2;
    public GameObject tier3;
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
    public Button upgradeInvalidButton;
    public Button deleteButton;
    public Transform cameraRef;
    public GameManager gm;
    public ResortPlayer player;
    public GameObject[] pivotPoints;
    public GameObject[] pivotPointsBase;
    void Start()
    {
        upgradeButton.onClick.AddListener(UpgradeListener);
        deleteButton.onClick.AddListener(DeleteListener);
        gm = GameManager.Instance;  
        player = ResortPlayer.Instance;
    }
    void UpgradeListener()
    {
        if(tier == 1 && gm.purchasableItems[1])
        {
            player.interactionAnimation();
            tier1.SetActive(false);
            tier2.SetActive(true);
            
            gm.pineapple--;
            gm.soup = gm.soup -3;
            tier++;
            fireRate = fireRate - fireRateDecrease;
            //setup range changer
            damage = damage + damageIncrease;
        }
        else if(tier == 2 && gm.purchasableItems[2])
        {
            tier2.SetActive(false);
            tier3.SetActive(true);
            
            gm.pineapple = gm.pineapple - 5;
            gm.soup = gm.soup - 4;
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
        if(currentTarget != null)
        {
            pivotPoints[tier-1].transform.LookAt(currentTarget);  
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
        if (other.CompareTag("Player"))
        {
            if (gm.purchasableItems[1] && tier == 1)
            {
                upgradeButton.gameObject.SetActive(true);
                upgradeInvalidButton.gameObject.SetActive(false);
            }
            else if (gm.purchasableItems[2] && tier == 2)
            {
                upgradeButton.gameObject.SetActive(true);
                upgradeInvalidButton.gameObject.SetActive(false);
            }
            else
            {
                upgradeButton.gameObject.SetActive(false);
                upgradeInvalidButton.gameObject.SetActive(true);
            }
        }
        if (other.CompareTag("Chimp"))
        {
            enemiesInRange.Add(other.transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            upgradeButton.gameObject.SetActive(false);
            upgradeInvalidButton.gameObject.SetActive(false);
        }
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