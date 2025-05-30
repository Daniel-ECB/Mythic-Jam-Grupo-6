using System.Collections.Generic;
using UnityEngine;

namespace MythicGameJam.Bullets
{
    public sealed class BulletPool : MonoBehaviour
    {
        public static BulletPool Instance;

        [System.Serializable]
        public struct BulletPrefabByType
        {
            public BulletType bulletType;
            public BulletBehaviour bulletPrefab;
        }


        public List<BulletPrefabByType> bulletPrefabs;

        private Dictionary<BulletType, Queue<BulletBehaviour>> poolDictionary;


        void Awake()
        {
            Instance = this;
            poolDictionary = new Dictionary<BulletType, Queue<BulletBehaviour>>();

            foreach (var bullet in bulletPrefabs)
            {
                Queue<BulletBehaviour> queue = new Queue<BulletBehaviour>();
                for (int i = 0; i < 10; i++)
                {
                    BulletBehaviour obj = Instantiate(bullet.bulletPrefab);
                    obj.gameObject.SetActive(false);
                    queue.Enqueue(obj);
                }
                poolDictionary.Add(bullet.bulletType, queue);
            }
        }

        public BulletBehaviour SpawnBullet(BulletType type, Vector3 position, Quaternion rotation)
        {
            BulletBehaviour bullet;

            var queue = poolDictionary[type];
            if (queue.Count > 0 && !queue.Peek().gameObject.activeInHierarchy)
            {
                bullet = queue.Dequeue();
            }
            else
            {
                bullet = Instantiate(GetPrefabByType(type));
            }

            bullet.transform.position = position;
            bullet.transform.rotation = rotation;
            bullet.gameObject.SetActive(true);
            queue.Enqueue(bullet);

            return bullet;
        }

        BulletBehaviour GetPrefabByType(BulletType type)
        {
            foreach (var b in bulletPrefabs)
                if (b.bulletType == type)
                    return b.bulletPrefab;
            return null;
        }
    }
}
