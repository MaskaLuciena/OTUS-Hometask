using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletSpawner : MonoBehaviour
    {
        [SerializeField] private int initialCount = 50;
        [SerializeField] private Transform container;
        [SerializeField] private Bullet prefab;
        [SerializeField] private Transform worldTransform;

        private readonly Queue<Bullet> bulletPool = new();

        public void CreatePool()
        {
            for (var i = 0; i < initialCount; i++)
            {
                var bullet = Instantiate(prefab, container);
                bulletPool.Enqueue(bullet);
            }
        }

        public Bullet SpawnBullet()
        {
            if (bulletPool.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(worldTransform);
            }
            else
            {
                bullet = Instantiate(prefab, worldTransform);
            }

            return bullet;
        }

        public void UnspawnBullet(Bullet bullet)
        {
            bullet.transform.SetParent(container);
            bulletPool.Enqueue(bullet);
        }
    }
}
