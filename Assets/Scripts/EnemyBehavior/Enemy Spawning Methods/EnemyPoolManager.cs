using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int initialPoolSize = 10;

    private Dictionary<GameObject, List<GameObject>> enemyPools = new Dictionary<GameObject, List<GameObject>>();

    public static EnemyPoolManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        foreach (GameObject prefab in enemyPrefabs)
        {
            if (!enemyPools.ContainsKey(prefab))
            {
                enemyPools[prefab] = new List<GameObject>();

                for (int i = 0; i < initialPoolSize; i++)
                {
                    GameObject enemy = Instantiate(prefab, transform);
                    enemy.SetActive(false);
                    enemyPools[prefab].Add(enemy);
                }
            }
        }
    }

    public GameObject GetEnemy(Vector3 spawnPosition)
    {
        if (enemyPrefabs.Length == 0)
        {
            Debug.LogWarning("No enemy prefabs assigned to EnemyPoolManager.");
            return null;
        }

        GameObject selectedPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        foreach (GameObject enemy in enemyPools[selectedPrefab])
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.transform.position = spawnPosition;
                enemy.SetActive(true);
                enemy.GetComponent<Enemy>().ResetEnemy();
                return enemy;
            }
        }

        GameObject newEnemy = Instantiate(selectedPrefab, transform);
        newEnemy.SetActive(false);
        enemyPools[selectedPrefab].Add(newEnemy);
        return GetEnemy(spawnPosition); 
    }

    public void ReturnToPool(GameObject enemy)
    {
        enemy.SetActive(false);
    }
}


