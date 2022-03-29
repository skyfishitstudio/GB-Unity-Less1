using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnProject
{
    public class Player : MonoBehaviour,ITakeDamage
    {
        public GameObject shieldPrefab;
        public Transform spawnPositionShield;
        public GameObject bombPrefab;
        public Transform spawnPositionBomb;
        public GameObject _bulletPrefab;
        public Transform spawnPositionBullet;
        private bool _IsSpawnShield;
        private bool _IsFire;
        private bool _ShieldOn;
        private Vector3 _direction;
        public float speed = 2f;
        public float _speedRotate;
        private bool _IsSprint;
        private bool _IsSpawnBomb;

        void Update()
        {
            if (Input.GetMouseButtonDown(0)) //�������� �� ����� ������ ���� 
                _IsFire = true;
            if (Input.GetMouseButtonDown(1)) //������ ��� �� ������ ������ ���� 
                _IsSpawnShield = true;
            if (Input.GetKeyDown(KeyCode.B)) //������ � ������� ����� �� ������ �
                _IsSpawnBomb = true;
            _IsSprint = Input.GetButton("Sprint");//���� ���
            _direction.x = Input.GetAxis("Horizontal"); //�������� ���� �����
            _direction.z = Input.GetAxis("Vertical");//�������� ���� ����
        
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
            //������� � ������� �������� ����� �������� ����� ����� ������ � �������� ���������� ��������
            transform.Rotate(new Vector3(0,(Input.GetAxis("Mouse X")* _speedRotate * Time.deltaTime) ,0));
        }

        private void SpawnBomb()
        {
            var bombObj = Instantiate(bombPrefab, spawnPositionBomb.position, spawnPositionBomb.rotation);
            var bomb = bombObj.GetComponent<Bomb>();
            bomb.Init(1); //�������������� ������ � �����
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
            var fixetDirection = transform.TransformDirection(_direction.normalized);
            transform.position += fixetDirection * (_IsSprint ? speed * 3 : speed) * delta;  
            
        }
        private void Fire()
        {
            if (PlayerManager.playerAmmo > 0)
            {
                var bulletObj = Instantiate(_bulletPrefab, spawnPositionBullet.position, spawnPositionBullet.rotation);//����� ����
                var bullet = bulletObj.GetComponent<Bullet>();//����� ������� ���� ��� ������� ��������� ����
                bullet.Init(spawnPositionBullet,10f,3f); //��������� :���� ����� � ������� �������� ������ �� ��������� 3f
                PlayerManager.playerAmmo--;
            }
            else
            {
                //������� ���������
            }

        }
        public void Hit(int damage)
        {
            PlayerManager.playerHealth -= damage;//�������� �� �������� ������ ���������� ����
        }
    }

}
