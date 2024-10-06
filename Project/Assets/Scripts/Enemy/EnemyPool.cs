using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPool : MonoBehaviour
    {

        [SerializeField]
        private GameObject prefab;

        public Transform container;

        public readonly Queue<GameObject> enemyPool = new();
        
        private void Awake()
        {
            for (var i = 0; i < 7; i++)
            {
                GameObject enemy = Instantiate(prefab, container);
                enemyPool.Enqueue(enemy);
            }
        }
    }
}