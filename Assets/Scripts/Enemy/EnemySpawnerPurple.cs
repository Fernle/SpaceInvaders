using UnityEngine;

public class EnemySpawnerPurple : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPositions;

    void Start()
    {
        SpawnAllEnemies();
    }

    void Update()
    {
        if (AreAllSpawnPositionsEmpty())
        {
            SpawnAllEnemies();
        }
    }

    void SpawnAllEnemies()
    {
        foreach (Transform spawnPosition in spawnPositions)
        {
            Instantiate(enemyPrefab, spawnPosition.position, Quaternion.identity);
        }
    }

    bool AreAllSpawnPositionsEmpty()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyPurple");

        return enemies.Length == 0;
    }
}
