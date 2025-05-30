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
        Vector3 spawnPosition = GetSpawnPositionOutsideView();
        EnemyPoolManager.Instance.GetEnemy(spawnPosition);
    }

    private Vector3 GetSpawnPositionOutsideView()
    {
        Camera cam = Camera.main;
        float camMinX = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float camMaxX = cam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        float camMaxY = cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

        int spawnSide = Random.Range(0, 3); 

        float x, y = camMaxY + 1f;

        if (spawnSide == 0)
        {
            x = Random.Range(camMinX, camMaxX);
        }
        else if (spawnSide == 1)
        {
            x = camMinX - 1f;
        }
        else
        {
            x = camMaxX + 1f;
        }

        return new Vector3(x, y, 0f);
    }
}


