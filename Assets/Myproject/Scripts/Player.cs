using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnProject
{
    public class Player : MonoBehaviour,ITakeDamage
    {
        [SerializeField] public GameObject shieldPrefab;
        [SerializeField] public Transform spawnPositionShield;
        [SerializeField] public GameObject bombPrefab;
        [SerializeField] public Transform spawnPositionBomb;
        [SerializeField] public GameObject _bulletPrefab;
        [SerializeField] public Transform spawnPositionBullet;
        private Rigidbody _rb;
        private Vector3 _direction;
        private bool _IsSpawnShield;
        private bool _IsFire;
        private bool _ShieldOn;
        public float _speed = 2f;
        public float _jumpForce;
        public float _speedRotate;
        private bool _IsSprint;
        private bool _IsSpawnBomb;
        public float _cfSpeed = 1f;
        public float sprint = 2f;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }
        void Update()
        {
            if (Input.GetMouseButtonDown(0)) //Стреляем по левой кнопке мыши 
                _IsFire = true;
            if (Input.GetMouseButtonDown(1)) //Ставим щит по правой кнопке мыши 
                _IsSpawnShield = true;
            if (Input.GetKeyDown(KeyCode.B)) //Ставим и убираем бомбу по кнопке В
                _IsSpawnBomb = true;
            if (Input.GetKeyDown(KeyCode.Space)) //Прыжок по кнопке В
                GetComponent<Rigidbody>().AddForce(Vector3.up * _jumpForce,ForceMode.Impulse);
            _IsSprint = Input.GetButton("Sprint");//Шифт бег
        }   
        void FixedUpdate()
        {
            if (_IsSpawnShield)
            {
                _IsSpawnShield = false;
                SpawnShield();
            }
            if (_IsSpawnBomb)
            {
                _IsSpawnBomb = false;
                SpawnBomb();
            }
            if (_IsFire)
            {
                _IsFire = false;
                Fire();
            }
            Move(Time.fixedDeltaTime);
            _direction.x = Input.GetAxis("Horizontal"); //движение лево право
            _direction.z = Input.GetAxis("Vertical");//движение вверх вниз
            _direction = transform.TransformDirection(_direction);
            //Поворот в сторону движения через движения мышью влево вправо с заданной скокростью поворота
            transform.Rotate(new Vector3(0,(Input.GetAxis("Mouse X")* _speedRotate * Time.deltaTime) ,0));
        }

        private void SpawnBomb()
        {
            var bombObj = Instantiate(bombPrefab, spawnPositionBomb.position, spawnPositionBomb.rotation);
            var bomb = bombObj.GetComponent<Bomb>();
            bomb.Init(1); //инициализируем прочку у бомбы
        }

        private void SpawnShield()
        {
            var shieldObj = Instantiate(shieldPrefab, spawnPositionShield.position, spawnPositionShield.rotation);
            var shield = shieldObj.GetComponent<Shield>();
            shield.Init(50 * PlayerManager.level);
            shield.transform.SetParent(spawnPositionShield);
        }
        private void Move(float delta)
        {
            //var fixetDirection = transform.TransformDirection(_direction.normalized);
            //transform.position += fixetDirection * (_IsSprint ? speed * 3 : speed) * delta;  
            _rb.MovePosition(transform.position + _direction.normalized * _speed * sprint * _cfSpeed * Time.fixedDeltaTime);
        }
        private void Fire()
        {
            if (PlayerManager.playerAmmo > 0)
            {
                var bulletObj = Instantiate(_bulletPrefab, spawnPositionBullet.position, spawnPositionBullet.rotation);//спаун пули
                var bullet = bulletObj.GetComponent<Bullet>();//поиск скрипта пули для задания парамеров пули
                bullet.Init(spawnPositionBullet,10f,3f); //параметры :пуля летит в сторону поворота игрока со скоростью 3f
                PlayerManager.playerAmmo--;
            }
            else
            {
                //патроны кончились
            }

        }
        public void Hit(int damage)
        {
            PlayerManager.playerHealth -= damage;//вычитаем из здоровья игрока полученный урон
        }
    }

}
