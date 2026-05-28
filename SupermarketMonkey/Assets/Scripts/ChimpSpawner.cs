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
    public GameObject enemyPrefab;
    public GameObject waypoints;
    public Transform cameraRef;

    [SerializeField] private waveInfo[] waves;

    public int wave = 0;

    private int currentlySpawnedChimps = 0;

    public bool spawnChimps = false;
    private bool isSpawning = false;

    void Update()
    {
        if (spawnChimps && !isSpawning)
        {
            StartCoroutine(SpawnChimps());
        }
    }

    IEnumerator SpawnChimps()
    {
        isSpawning = true;

        waveInfo currentWave = waves[wave];

        currentlySpawnedChimps = 0;

        while (currentlySpawnedChimps < currentWave.amountToSpawn)
        {
            SpawnEnemy();

            yield return new WaitForSeconds(currentWave.spawnRate);
        }

        Debug.Log("Wave Complete");

        spawnChimps = false;
        isSpawning = false;

        wave++;
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
        }

        currentlySpawnedChimps++;
    }
}