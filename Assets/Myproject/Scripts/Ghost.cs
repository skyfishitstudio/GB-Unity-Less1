using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnProject
{
    public class Ghost : MonoBehaviour,ITakeDamage
    {
        [SerializeField] private Player _player;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _spawnPositionBullet;
        [SerializeField] private int _enemylevel;// ������� ��������
        [SerializeField] private float _enemyhealth = 50f; // �������� ��������
        [SerializeField] private float _cooldown = 1f; // ����� ����� ����������
        [SerializeField] private bool _IsFire;
        void Start()
        {
            _player = FindObjectOfType<Player>();
        }

        
        
        public void Init(float enemyhealth,int enemylevel) //������������� �������� ��� ������ ��������
        {
            enemyhealth = _enemyhealth;
            enemylevel = _enemylevel;
            //Destroy(gameObject, 15f); ���� ����� ��� ������������ �������� ����� �������� �����
        }
        private void FixedUpdate()
        {
            //������� � ������� ������ ���� ���������� ������ 4
            if( Vector3.Distance(transform.position, _player.transform.position) < 5)
            {
                transform.LookAt(_player.transform.position); 
            }
            //�������� � ������� ������ ���� ���������� ������ 2
            if (Vector3.Distance(transform.position, _player.transform.position) < 3)
            {
                if(_IsFire) 
                    Fire();
            }
        }
        private void Fire()
        {
            _IsFire = false;
            var bulletObj = Instantiate(_bulletPrefab, _spawnPositionBullet.position, _spawnPositionBullet.rotation);//����� ����
            var bullet = bulletObj.GetComponent<Bullet>();//����� ������� ���� ��� ������� ��������� ����
            bullet.Init(_player.transform,10f,3f); //��������� :���� ����� � ������� ������ �� ��������� 3
            Invoke(nameof(Reloading), _cooldown);
        }

        private void Reloading()
        {
            _IsFire = true;

        }
        public void Hit(int damage)
        {
            _enemyhealth -= damage;//�������� �� �������� �������� ���������� ����
            if (_enemyhealth <=0) //����� �������� ������ ��� ����� 0
                Destroy(gameObject, 1f); // ������ ������ �� ����� ����� ���
        }
    }
}

