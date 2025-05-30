using MythicGameJam.Enemies;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 2f;
    private float spawnTimer = 0f;

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = GetRandomPositionInView();
        EnemyPoolManager.Instance.GetEnemy(spawnPosition);
    }

    private Vector3 GetRandomPositionInView()
    {
        Camera cam = Camera.main;
        float minX = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float maxX = cam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        float minY = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        float maxY = cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);

        return new Vector3(x, y, 0f);
    }
}

