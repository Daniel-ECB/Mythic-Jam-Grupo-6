using MythicGameJam.Bullets;
using MythicGameJam.Input;
using UnityEngine;

namespace MythicGameJam.Player
{
    public sealed class PlayerShooter : MonoBehaviour
    {
        [SerializeField]
        private GameObject bulletPrefab;
        [SerializeField]
        private Transform firePoint; // Where the bullet spawns (e.g., at the tip of the weapon)
        [SerializeField]
        private float shootCooldown = 0.2f;

        private float _lastShootTime;

        private void OnEnable()
        {
            InputManager.Instance.OnAttack += HandleShoot;
        }

        private void OnDisable()
        {
            if (InputManager.Instance != null)
                InputManager.Instance.OnAttack -= HandleShoot;
        }

        private void HandleShoot()
        {
            if (Time.time - _lastShootTime < shootCooldown)
                return;

            _lastShootTime = Time.time;

            // Determine spawn position and direction
            Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position;
            Vector2 shootDirection = transform.right; // In Unity 2D, right (X) is the "forward" direction

            // Spawn bullet
            GameObject bulletObj = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
            var bullet = bulletObj.GetComponent<BulletBehaviour>();
            if (bullet != null)
                bullet.SetDirection(shootDirection);
        }
    }
}
