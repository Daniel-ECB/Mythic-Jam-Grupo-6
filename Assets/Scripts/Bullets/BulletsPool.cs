using System.Collections.Generic;
using UnityEngine;

namespace MythicGameJam.Bullets
{
    public sealed class BulletsPool : Core.Utils.Singleton<BulletsPool>
    {
        [System.Serializable]
        public class BulletPoolEntry
        {
            public GameObject bulletPrefab;
            public int initialSize = 10;
        }

        [Header("Bullet Types")]
        [SerializeField]
        private List<BulletPoolEntry> bulletTypes;

        private readonly Dictionary<GameObject, Queue<GameObject>> _pools = new();

        protected override void Awake()
        {
            base.Awake();
            foreach (var entry in bulletTypes)
            {
                var queue = new Queue<GameObject>();
                for (int i = 0; i < entry.initialSize; i++)
                {
                    var bullet = Instantiate(entry.bulletPrefab, transform);
                    bullet.SetActive(false);
                    queue.Enqueue(bullet);
                }
                _pools[entry.bulletPrefab] = queue;
            }
        }

        public GameObject GetBullet(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            if (!_pools.ContainsKey(prefab))
            {
                // If prefab not registered, create a new pool for it
                _pools[prefab] = new Queue<GameObject>();
            }

            GameObject bullet;
            if (_pools[prefab].Count > 0)
            {
                bullet = _pools[prefab].Dequeue();
            }
            else
            {
                bullet = Instantiate(prefab);
            }

            bullet.transform.position = position;
            bullet.transform.rotation = rotation;
            bullet.SetActive(true);
            return bullet;
        }

        public void ReturnBullet(GameObject prefab, GameObject bullet)
        {
            bullet.SetActive(false);
            _pools[prefab].Enqueue(bullet);
        }
    }
}