using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;

        [NonSerialized] public bool isPlayer;
        [NonSerialized] public int damage;

        [SerializeField] private new Rigidbody2D rigidbody2D;
       
        [SerializeField] private SpriteRenderer spriteRenderer;      

        private void OnCollisionEnter2D(Collision2D collision)
        {
             OnCollisionEntered?.Invoke(this, collision);
        }

        public void SetBulletArgs(Args args)
        {
            SetVelocity(args.velocity);
            SetPosition(args.position);
            SetColor(args.color);
            SetPhysicsLayer(args.physicsLayer);
            damage = args.damage;
            isPlayer = args.isPlayer;
            SetVelocity(args.velocity);
        }
        private void SetVelocity(Vector2 velocity)
        {
            rigidbody2D.velocity = velocity;
        }

        private void SetPhysicsLayer(int physicsLayer)
        {
            gameObject.layer = physicsLayer;
        }

        private void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        private void SetColor(Color color)
        {
            spriteRenderer.color = color;
        }

        public struct Args
        {
            public Vector2 position;
            public Vector2 velocity;
            public Color color;
            public int physicsLayer;
            public int damage;
            public bool isPlayer;
        }
    }
}