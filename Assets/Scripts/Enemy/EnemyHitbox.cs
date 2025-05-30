using MythicGameJam.Bullets;
using MythicGameJam.Core.GameManagement;
using UnityEngine;

namespace MythicGameJam.Enemies
{
    public sealed class EnemyHitbox : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PlayerBullet"))
            {
                GameplayManager.Instance.RegisterEnemyKill();

                // Return enemy to pool via EnemiesManager
                EnemiesManager.Instance.ReturnToPool(gameObject);

                // Return bullet to pool
                var bullet = other.GetComponent<BulletBehaviour>();
                if (bullet != null) bullet.ReturnToPool();
            }
        }
    }
}
