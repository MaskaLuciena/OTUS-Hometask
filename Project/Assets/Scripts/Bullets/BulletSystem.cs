using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField] private LevelBounds levelBounds;
        [SerializeField] private BulletSpawner bulletSpawner;

        private readonly HashSet<Bullet> activeBullets = new();
        private readonly List<Bullet> cache = new();

        private void Awake()
        {
            bulletSpawner.CreatePool();
        }

        private void FixedUpdate()
        {
            BulletOutOfBounds();
        }

        public void FlyBulletByArgs(Bullet.Args args)
        {
            Bullet bullet = bulletSpawner.SpawnBullet();
            
            bullet.SetBulletArgs(args);

            if (activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += OnBulletCollision;
            }
        }

        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            BulletUtils.DealDamage(bullet, collision.gameObject);
            RemoveBullet(bullet);
        }

        private void BulletOutOfBounds()
        {
            cache.Clear();
            cache.AddRange(activeBullets);

            for (int i = 0, count = cache.Count; i < count; i++)
            {
                Bullet bullet = cache[i];
                if (!levelBounds.InBounds(bullet.transform.position))
                {
                    RemoveBullet(bullet);
                }
            }
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= OnBulletCollision;
                bulletSpawner.UnspawnBullet(bullet);
            }
        }
    }
}