using UnityEngine;

namespace MythicGameJam.Bullets
{
    public class ShooterDemo : MonoBehaviour
    {
        public BulletType bulletType = BulletType.Human;
        public float shootCooldown = 0.2f;

        private float timer;

        void Update()
        {
            timer += Time.deltaTime;

            if (UnityEngine.Input.GetKey(KeyCode.Space) && timer >= shootCooldown)
            {
                BulletPool.Instance.SpawnBullet(bulletType, transform.position, Quaternion.identity);
                timer = 0f;
            }
        }
    }
}
