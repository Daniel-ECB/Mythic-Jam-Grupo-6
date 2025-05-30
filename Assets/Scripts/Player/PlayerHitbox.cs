using MythicGameJam.Bullets;
using UnityEngine;

namespace MythicGameJam.Player
{
    public sealed class PlayerHitbox : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("EnemyBullet"))
            {
                PlayerFormManager.Instance.RegisterHit();
                // Return bullet to pool
                var bullet = other.GetComponent<BulletBehaviour>();
                if (bullet != null) bullet.ReturnToPool();
            }
        }
    }
}
