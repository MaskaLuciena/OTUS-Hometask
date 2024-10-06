using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour
    {
        [SerializeField]
        private float startPositionY;

        [SerializeField]
        private float endPositionY;

        [SerializeField]
        private float movingSpeedY;

        private float positionX;

        private float positionZ;
      
        private void Awake()
        {
            positionX = transform.position.x;
            positionZ = transform.position.z;
        }

        private void FixedUpdate()
        {
            BackGroundMove();
        }

        private void BackGroundMove()
        {
            if (transform.position.y <= endPositionY)
            {
                transform.position = new Vector3(positionX, startPositionY, positionZ);
            }

            transform.position -= new Vector3(positionX, movingSpeedY * Time.fixedDeltaTime, positionZ);
        }
    }
}