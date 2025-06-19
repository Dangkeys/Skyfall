using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Enemy enemy;
    [SerializeField] private float spawnInterval = 4f;
    [SerializeField] private float minSpawnInterval = 1.5f;
    [SerializeField] private float decreaseIntervalAmount = 0.05f;
    [SerializeField] private LevelManager levelManager;

    private float timer;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f && !levelManager.GetIsGameOver())
        {
            IncreaseDificulty();
            SpawnEnemy();
            timer = spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemy, randomSpawnPoint.position, Quaternion.identity);
    }

    private void IncreaseDificulty()
    {
        if (spawnInterval > minSpawnInterval)
        {
            spawnInterval -= decreaseIntervalAmount;
        }
    }
}
