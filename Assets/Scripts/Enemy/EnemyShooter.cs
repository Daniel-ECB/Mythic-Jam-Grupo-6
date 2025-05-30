using UnityEngine;
using MythicGameJam.Bullets;

namespace MythicGameJam.Enemies
{
    public sealed class EnemyShooter : MonoBehaviour
    {
        [SerializeField]
        private GameObject bulletPrefab;
        [SerializeField]
        private float shootInterval = 3f;
        [SerializeField]
        private float spawnRadius = 1.2f;

        private float _lastShootTime;

        private void Update()
        {
            if (Time.time - _lastShootTime >= shootInterval)
            {
                Shoot();
                _lastShootTime = Time.time;
            }
        }

        private void Shoot()
        {
            // Pick a random angle in radians
            float angle = Random.Range(0f, Mathf.PI * 2f);
            Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * spawnRadius;
            Vector2 spawnPos = (Vector2)transform.position + offset;
            Vector2 shootDirection = offset.normalized;

            GameObject bulletObj = BulletsPool.Instance.GetBullet(bulletPrefab, spawnPos, Quaternion.identity);
            var bullet = bulletObj.GetComponent<BulletBehaviour>();
            if (bullet != null)
            {
                bullet.SetDirection(shootDirection);
                bullet.SetPrefabReference(bulletPrefab);
            }
        }
    }
}
