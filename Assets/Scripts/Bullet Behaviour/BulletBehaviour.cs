using UnityEngine;

namespace MythicGameJam.Bullets
{
    public sealed class BulletBehaviour : MonoBehaviour
    {
        [SerializeField]
        private float speed = 10f;
        [SerializeField]
        private float lifeTime = 3f;

        private float timer;
        private Vector2 _direction = Vector2.up; // Default direction
        private GameObject _prefabReference;

        /// <summary>
        /// Set the direction of the bullet. Should be called immediately after spawning.
        /// </summary>
        public void SetDirection(Vector2 direction)
        {
            _direction = direction.normalized;
        }

        /// <summary>
        /// Set the prefab reference for pooling.
        /// </summary>
        public void SetPrefabReference(GameObject prefab)
        {
            _prefabReference = prefab;
        }

        void OnEnable()
        {
            timer = 0f;
        }

        void Update()
        {
            transform.Translate(_direction * speed * Time.deltaTime, Space.World);
            timer += Time.deltaTime;

            if (timer >= lifeTime)
                ReturnToPool();
        }

        public void ReturnToPool()
        {
            if (_prefabReference != null)
                BulletsPool.Instance.ReturnBullet(_prefabReference, gameObject);
            else
                Destroy(gameObject);
        }
    }
}
