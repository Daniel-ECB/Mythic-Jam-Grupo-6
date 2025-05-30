using System.Collections.Generic;
using UnityEngine;
using MythicGameJam.Core.Utils;

namespace MythicGameJam.Enemies
{
    public sealed class EnemyPoolManager : Singleton<EnemyPoolManager>
    {
        [SerializeField]
        private GameObject[] enemyPrefabs;

        [SerializeField]
        private int initialPoolSize = 10;

        private List<GameObject> enemyPool = new List<GameObject>();

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            for (int i = 0; i < initialPoolSize; i++)
            {
                GameObject prefab = GetRandomEnemyPrefab();
                GameObject enemy = Instantiate(prefab, transform);
                enemy.SetActive(false);
                enemyPool.Add(enemy);
            }
        }

        public GameObject GetEnemy(Vector3 spawnPosition)
        {
            foreach (GameObject enemy in enemyPool)
            {
                if (!enemy.activeInHierarchy)
                {
                    enemy.transform.position = spawnPosition;
                    enemy.SetActive(true);
                    enemy.GetComponent<Enemy>().ResetEnemy();
                    return enemy;
                }
            }

            GameObject prefab = GetRandomEnemyPrefab();
            GameObject newEnemy = Instantiate(prefab, transform);
            newEnemy.SetActive(false);
            enemyPool.Add(newEnemy);
            return GetEnemy(spawnPosition);
        }

        public void ReturnToPool(GameObject enemy)
        {
            enemy.SetActive(false);
        }

        private GameObject GetRandomEnemyPrefab()
        {
            if (enemyPrefabs == null || enemyPrefabs.Length == 0)
            {
                Debug.LogError("EnemyPoolManager: No enemy prefabs assigned.");
                return null;
            }

            return enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        }
    }
}

