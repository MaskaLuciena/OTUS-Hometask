using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameObject character;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private BulletSystem bulletSystem;
        [SerializeField] private BulletConfig bulletConfig;

        public bool fire;
        private void OnEnable()
        {
            character.GetComponent<HitPointsComponent>().HpEmpty += OnDeath;
        }

        private void OnDisable()
        {
            character.GetComponent<HitPointsComponent>().HpEmpty -= OnDeath;
        }

        private void FixedUpdate()
        {   
            if (fire)
            {
                OnShoot();
                fire = false;
            }
        }
        private void OnDeath(GameObject _)
        {
            gameManager.FinishGame();
        }

        private void OnShoot()
        {
            var weapon = character.GetComponent<WeaponComponent>();
            bulletSystem.FlyBulletByArgs(new Bullet.Args
            {
                isPlayer = true,
                physicsLayer = (int) bulletConfig.physicsLayer,
                color = bulletConfig.color,
                damage = bulletConfig.damage,
                position = weapon.Position,
                velocity = weapon.Rotation * Vector3.up * bulletConfig.speed
            });
        }
    }
}