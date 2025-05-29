using UnityEngine;

public class SingleShotSpawning : MonoBehaviour, IEnemyAttack
{
    [SerializeField] private GameObject[] bulletPrefabs;
    [SerializeField] private int[] healthIndexes;

    public int[] HealthIndexes => healthIndexes;
    public EnemyAttackController enemyAttackManager { get; set; }

    public void Attack()
    {
        if (bulletPrefabs == null || bulletPrefabs.Length == 0) return;

        Enemy enemy = GetComponent<Enemy>();
        if (enemy == null) return;

        if (System.Array.Exists(healthIndexes, index => index == enemy.HealthIndex))
        {
            Vector3 spawnPosition = transform.position;

            Vector2 direction = Vector2.right; 
            if (enemyAttackManager != null && enemyAttackManager.LastMovementDirection != Vector2.zero)
            {
                direction = enemyAttackManager.LastMovementDirection.normalized;
            }

            foreach (GameObject prefab in bulletPrefabs)
            {
                GameObject bullet = Instantiate(prefab, spawnPosition, Quaternion.identity);

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }
}

