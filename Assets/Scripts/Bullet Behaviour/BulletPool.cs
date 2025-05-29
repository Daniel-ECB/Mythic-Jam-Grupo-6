using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    [System.Serializable]
    public struct BulletPrefabByType
    {
        public BulletType bulletType;
        public GameObject bulletPrefab;
    }

    public List<BulletPrefabByType> bulletPrefabs;

    private Dictionary<BulletType, Queue<GameObject>> poolDictionary;

    void Awake()
    {
        Instance = this;
        poolDictionary = new Dictionary<BulletType, Queue<GameObject>>();

        foreach (var bullet in bulletPrefabs)
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < 10; i++)
            {
                GameObject obj = Instantiate(bullet.bulletPrefab);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }
            poolDictionary.Add(bullet.bulletType, queue);
        }
    }

    public GameObject SpawnBullet(BulletType type, Vector3 position, Quaternion rotation)
    {
        GameObject bullet;

        var queue = poolDictionary[type];
        if (queue.Count > 0 && !queue.Peek().activeInHierarchy)
        {
            bullet = queue.Dequeue();
        }
        else
        {
            bullet = Instantiate(GetPrefabByType(type));
        }

        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        bullet.SetActive(true);
        queue.Enqueue(bullet);

        return bullet;
    }

    GameObject GetPrefabByType(BulletType type)
    {
        foreach (var b in bulletPrefabs)
            if (b.bulletType == type)
                return b.bulletPrefab;
        return null;
    }
}
