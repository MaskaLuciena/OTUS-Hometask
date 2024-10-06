using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        public event Action<GameObject> HpEmpty;
        
        [SerializeField]
        private int hitPoints;

        private int hp;

        private void Awake()
        {
            hp = hitPoints;
        }
        public bool IsHitPointsExists()
        {
            return hitPoints > 0;
        }

        public void TakeDamage(int damage)
        {            
            hitPoints -= damage;
            if (hitPoints <= 0)
            {
                HpEmpty?.Invoke(gameObject);
                hitPoints = hp;
            }
        }
    }
}