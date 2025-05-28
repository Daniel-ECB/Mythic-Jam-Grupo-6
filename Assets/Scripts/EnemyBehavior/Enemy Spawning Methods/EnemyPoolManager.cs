using System.Collections.Generic;
using UnityEngine;
using MythicGameJam.Core.Utils;

namespace MythicGameJam.Enemies
{
    public sealed class EnemyPoolManager : Singleton<EnemyPoolManager>
    {
        [SerializeField]
        private GameObject enemyPrefab;

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
                GameObject enemy = Instantiate(enemyPrefab, transform);
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

            GameObject newEnemy = Instantiate(enemyPrefab, transform);
            newEnemy.SetActive(false);
            enemyPool.Add(newEnemy);
            return GetEnemy(spawnPosition);
        }

        public void ReturnToPool(GameObject enemy)
        {
            enemy.SetActive(false);
        }
    }
}
