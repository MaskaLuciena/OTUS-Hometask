using UnityEngine;

namespace ShootEmUp
{
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D characterRb2D;
        
        [SerializeField]
        private InputManager inputManager;

        [SerializeField]
        private float speed = 0.2f;

        private void FixedUpdate()
        {
            MoveCharacter();
        }
        private void MoveCharacter()
        {
            float direction = inputManager.GetComponent<InputManager>().HorizontalDirection;

            Vector2 nextPosition = characterRb2D.position + new Vector2(direction, 0) * speed;

            characterRb2D.MovePosition(nextPosition);
        }
    }
}