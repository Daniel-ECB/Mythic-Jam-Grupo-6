using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MythicGameJam.Enemies
{
    public sealed class EnemiesManager : Core.Utils.Singleton<EnemiesManager>
    {
        [SerializeField]
        private List<Transform> waypoints;
        [SerializeField]
        private GameObject asuraPrefab;
        [SerializeField]
        private GameObject pretaPrefab;
        [SerializeField]
        private float spawnInterval = 3f;
        [SerializeField]
        private int maxEnemies = 10;

        private readonly List<GameObject> _activeEnemies = new();
        private readonly Queue<GameObject> _asuraPool = new();
        private readonly Queue<GameObject> _pretaPool = new();

        private void Start()
        {
            StartCoroutine(SpawnEnemiesRoutine());
        }

        private IEnumerator SpawnEnemiesRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnInterval);
                if (_activeEnemies.Count < maxEnemies)
                    SpawnRandomEnemy();
            }
        }

        private void SpawnRandomEnemy()
        {
            bool spawnAsura = Random.value < 0.5f;
            GameObject prefab = spawnAsura ? asuraPrefab : pretaPrefab;
            Queue<GameObject> pool = spawnAsura ? _asuraPool : _pretaPool;

            Transform spawnPoint = GetRandomWaypoint();
            GameObject enemy;

            if (pool.Count > 0)
            {
                enemy = pool.Dequeue();
                enemy.transform.position = spawnPoint.position;
                enemy.transform.rotation = Quaternion.identity;
                enemy.SetActive(true);
            }
            else
            {
                enemy = Instantiate(prefab, spawnPoint.position, Quaternion.identity, transform);
            }

            // Ensure parent for hierarchy cleanliness
            enemy.transform.SetParent(transform);

            _activeEnemies.Add(enemy);

            // Register for destruction callback
            var enemyController = enemy.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.OnEnemyDestroyed -= HandleEnemyDestroyed; // Avoid double subscription
                enemyController.OnEnemyDestroyed += HandleEnemyDestroyed;
            }
        }

        public Transform GetRandomWaypoint()
        {
            if (waypoints.Count == 0) return null;
            return waypoints[Random.Range(0, waypoints.Count)];
        }

        private void HandleEnemyDestroyed(GameObject enemy)
        {
            if (_activeEnemies.Contains(enemy))
                _activeEnemies.Remove(enemy);

            // Return to the correct pool
            if (enemy.CompareTag("Asura"))
                _asuraPool.Enqueue(enemy);
            else if (enemy.CompareTag("Preta"))
                _pretaPool.Enqueue(enemy);

            enemy.SetActive(false);
        }
    }
}
