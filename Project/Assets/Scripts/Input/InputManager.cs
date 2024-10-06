using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        public float HorizontalDirection { get; private set; }
        public bool IsShoot { get; private set; }

        private void Update()
        {
            ShootInput();
            MoveInput();
        }
        
        private float MoveInput()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                HorizontalDirection = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                HorizontalDirection = 1;
            }
            else
            {
                HorizontalDirection = 0;
            }

            return HorizontalDirection;
        }

        private void ShootInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {   
                characterController.fire = true;
            }
        }
    }
}