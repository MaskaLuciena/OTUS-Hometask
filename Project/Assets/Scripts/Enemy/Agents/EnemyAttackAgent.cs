using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        public delegate void FireHandler(GameObject enemy, Vector2 position, Vector2 direction);

        public event FireHandler OnFire;

        [SerializeField] private WeaponComponent weaponComponent;
        [SerializeField] private EnemyMoveAgent moveAgent;
        [SerializeField] private float countdown;

        private GameObject target;
        private float currentTime;

        public void Reset()
        {
            currentTime = countdown;
        }

        private void FixedUpdate()
        {
            FireWithCountdown();
        }

        public void SetTarget(GameObject target)
        {
            this.target = target;
        }

        private void FireWithCountdown()
        {
            if (!moveAgent.IsReached)
            {
                return;
            }

            if (!target.GetComponent<HitPointsComponent>().IsHitPointsExists())
            {
                return;
            }

            currentTime -= Time.fixedDeltaTime;

            if (currentTime <= 0)
            {
                Fire();
                currentTime += countdown;
            }
        }
        private void Fire()
        {
            Vector2 startPosition = weaponComponent.Position;
            Vector2 vector = (Vector2)target.transform.position - startPosition;
            Vector2 direction = vector.normalized;

            OnFire?.Invoke(gameObject, startPosition, direction);
        }
    }
}