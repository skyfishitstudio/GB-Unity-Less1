using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        [SerializeField] private UnityEvent _event;
        //[SerializeField] private List<GameObject> _patrolPoint;
        //[SerializeField] private List<Transform> _patrolList;
        private Transform _pp;
        private Rigidbody _rigidbody;
        private Vector3 _direction;
        //private float _speed = 2f;
        //private float _cfSpeed = 1f;

        void Awake()
        {
            //_patrolList = new List<Transform>(); //������������� ������ ������� �������� ��������������
            //_rigidbody = GetComponent<Rigidbody>(); // ������������� ���������� ������ � ��������
            //��������� ������ ����� �������������� ���� Transform �� ����������� � ��������� ������ GameObject
            //foreach (GameObject nextObjPp in _patrolPoint)
            //{
            // _pp = nextObjPp.transform;
            // _patrolList.Add(_pp);
            //}
        }
       
        void Start()
        {
            _player = FindObjectOfType<Player>();
           //StartCoroutine(Patrol());
        }
                  
        public void Init(float enemyhealth,int enemylevel) //������������� �������� ��� ������ ��������
        {
            enemyhealth = _enemyhealth;
            enemylevel = _enemylevel;
            //Destroy(gameObject, 15f); ���� ����� ��� ������������ �������� ����� �������� �����
        }
        private void FixedUpdate()
        {
            Ray ray = new Ray(_spawnPositionBullet.position, transform.forward); //������ �������� �� 4 ������
            
            if (Physics.Raycast(ray, out RaycastHit hit, 4))
            {
                Debug.DrawRay(_spawnPositionBullet.position, transform.forward * hit.distance, Color.blue); //�����
                if (hit.collider.CompareTag("Player")) //�������� � ������� ������ ���� ���������� ������ 4 � ���� ���������
                {
                    if (_IsFire)
                        Fire();
                }
            }
            //������� � ������� ������ ���� ���������� ������ 5 (�������� �����)
            if( Vector3.Distance(transform.position, _player.transform.position) < 5)
            {
                transform.LookAt(_player.transform.position); 
            }
            //�������� �������� � ����������� _direction
            //Move(Time.fixedDeltaTime);
        }
        private void Fire()
        {
            _IsFire = false;
            var bulletObj = Instantiate(_bulletPrefab, _spawnPositionBullet.position, _spawnPositionBullet.rotation);//����� ����
            var bullet = bulletObj.GetComponent<Bullet>();//����� ������� ���� ��� ������� ��������� ����
            bullet.Init(_player.transform,10f,3f); //��������� :���� ����� � ������� ������ �� ��������� 3
            Invoke(nameof(Reloading), _cooldown);
            //_event?.Invoke();
        }
        private void Move(float delta)
        {
            //var fixetDirection = transform.TransformDirection(_direction.normalized);
            //transform.position += fixetDirection * (_IsSprint ? speed * 3 : speed) * delta;  
            //_rigidbody.MovePosition(transform.position + _direction.normalized * _speed * _cfSpeed * delta);
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
        //private IEnumerator Patrol()
        //{
        //    Console.WriteLine("Wait start");
        //    yield return new WaitForSeconds(5f); //����� ����� ������� �������
        //    Console.WriteLine("Start");
        //    foreach (Transform nextTrnsPp in _patrolList)
        //    {
        //        Console.WriteLine("Wait rotate");
        //        yield return new WaitForSeconds(5f); //����� ����� ������� ��������
        //        Console.WriteLine("Rotate");
        //        transform.LookAt(nextTrnsPp.transform.position);
        //        Console.WriteLine("Wait move");
        //        yield return new WaitForSeconds(5f); //����� ����� ������� �������� �� ��������
        //        Console.WriteLine("Move");
        //        _direction = nextTrnsPp.transform.position;
        //        yield return new WaitForSeconds(5f); //����� ����� ������������ �������� �� ��������
        //    }
        //}
    }
}

