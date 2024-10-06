using UnityEngine;
using UnityEngine.TextCore.Text;

namespace ShootEmUp
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyPool EnemyPool;       

        [SerializeField] private Transform worldTransform;        

        [SerializeField] private EnemyPositions enemyPositions;       
        [SerializeField] private GameObject character;       
        public GameObject SpawnEnemy()
        {
            if (!EnemyPool.enemyPool.TryDequeue(out var enemy))
            {
                return null;
            }

            enemy.transform.SetParent(worldTransform);

            Transform spawnPosition = enemyPositions.RandomSpawnPosition();

            enemy.transform.position = spawnPosition.position;
            enemy.GetComponent<EnemyAttackAgent>().SetTarget(character);

            return enemy;
        }
        public void UnspawnEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(EnemyPool.container);
            EnemyPool.enemyPool.Enqueue(enemy);
        }
    }
}
