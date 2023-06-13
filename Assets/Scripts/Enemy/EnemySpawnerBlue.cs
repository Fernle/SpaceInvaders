using UnityEngine;

public class EnemySpawnerBlue : MonoBehaviour
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
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyBlue");

        return enemies.Length == 0;
    }
}
