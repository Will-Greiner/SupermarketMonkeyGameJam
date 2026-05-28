using System.Collections;
using UnityEngine;

[System.Serializable]
public class waveInfo
{
    public int amountToSpawn;
    public float spawnRate;
}

public class ChimpSpawner : MonoBehaviour
{
    private bool waveStarted = false;
    public static ChimpSpawner Instance;
    public GameObject endTowerScreen;
    public GameObject enemyPrefab;
    public GameObject waypoints;
    public Transform cameraRef;

    [SerializeField] private waveInfo[] waves;

    public int wave = 0;

    public int currentlySpawnedChimps = 0;
    public int totalSpawnedChimps = 0;

    public bool spawnChimps = false;
    private bool isSpawning = false;

    void Awake()
    {
        Instance = this;
    }

    void Update()
{
    if (spawnChimps && !isSpawning)
    {
        waveStarted = true;  // mark that a wave has begun
        StartCoroutine(SpawnChimps());
    }

    // Victory condition: wave started, all chimps spawned AND all dead
    if (waveStarted && !spawnChimps && !isSpawning && currentlySpawnedChimps <= 0)
    {
        endTowerScreen.SetActive(true);
        wave++;
        waveStarted = false; // reset for next wave
    }
}

    IEnumerator SpawnChimps()
    {
        isSpawning = true;

        waveInfo currentWave = waves[wave];

        totalSpawnedChimps = 0;

        while (totalSpawnedChimps < currentWave.amountToSpawn)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(currentWave.spawnRate);
        }

        // Finished spawning, but chimps are still alive
        isSpawning = false;
        spawnChimps = false;
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(
            enemyPrefab,
            transform.position,
            Quaternion.identity
        );

        Billboard chimpBillboard = enemy.GetComponentInChildren<Billboard>();
        ChimpSystem chimp = enemy.GetComponent<ChimpSystem>();

        if (chimp != null)
        {
            chimpBillboard.mainCameraTransform = cameraRef;
            chimp.waypoints = waypoints;

            // Tell the chimp about this spawner
            chimp.spawner = this;
        }

        currentlySpawnedChimps++;
        totalSpawnedChimps++;
    }

    // Call this from chimp when it dies
    public void OnChimpDeath()
    {
        currentlySpawnedChimps--;
    }
}