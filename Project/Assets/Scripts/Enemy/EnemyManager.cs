using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {        
        [SerializeField] private EnemySpawner enemySpawner;

        [SerializeField] private BulletSystem bulletSystem;        

        [SerializeField] private BulletConfig bulletConfig;

        private readonly HashSet<GameObject> activeEnemies = new();

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(2);

                GameObject enemy = enemySpawner.SpawnEnemy();

                if (enemy != null)
                {
                    if (activeEnemies.Add(enemy))
                    {
                        enemy.GetComponent<HitPointsComponent>().HpEmpty += OnDestroyed;
                        enemy.GetComponent<EnemyAttackAgent>().OnFire += OnFire;
                    }    
                }
            }
        }

        private void OnDestroyed(GameObject enemy)
        {
            if (activeEnemies.Remove(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().HpEmpty -= OnDestroyed;
                enemy.GetComponent<EnemyAttackAgent>().OnFire -= OnFire;

                enemySpawner.UnspawnEnemy(enemy);
            }
        }

        private void OnFire(GameObject enemy, Vector2 position, Vector2 direction)
        {
            bulletSystem.FlyBulletByArgs(new Bullet.Args
            {
                isPlayer = false,
                physicsLayer = (int) bulletConfig.physicsLayer,
                color = Color.red,
                damage = bulletConfig.damage,
                position = position,
                velocity = direction * bulletConfig.speed
            });
        }
    }
}