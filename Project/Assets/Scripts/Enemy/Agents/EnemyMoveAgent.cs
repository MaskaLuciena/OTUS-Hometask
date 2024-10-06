using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        private Vector2 destination;
        private Vector2 direction;

        private float speed = 5;

        public bool IsReached;

        private void FixedUpdate()
        {            
            MoveToDestinationPoint();
        }

        private void OnDisable()
        {
            IsReached = false;
        }

        private void SetDestination()
        {
            destination = (Vector2)EnemyPositions.enemyPositions.RandomAttackPosition().position;      
            IsReached = false;
        }

        private void MoveToDestinationPoint()
        {
            if (IsReached)
            {
                return;
            }

            if (destination == new Vector2(0, 0))
            {
                SetDestination();
            }

            Vector2 vector = destination - (Vector2)transform.position;

            if (vector.magnitude <= 0.25f)
            {
                IsReached = true;
                return;
            }

            direction = vector.normalized * Time.fixedDeltaTime;
            MoveEnemy(direction);
        }

        private void MoveEnemy(Vector2 vector)
        {
            Vector2 nextPosition = (Vector2)gameObject.transform.position + vector * speed;
            transform.position = nextPosition;
        }
    }
}