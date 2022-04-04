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
        [SerializeField] private int _enemylevel;// уровень призрака
        [SerializeField] private float _enemyhealth = 50f; // здоровье призрака
        [SerializeField] private float _cooldown = 1f; // время между выстрелами
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
            //_patrolList = new List<Transform>(); //Инициализация списка позиций маршрута патрулирования
            //_rigidbody = GetComponent<Rigidbody>(); // Инициализация компонента физики у призрака
            //Заполняем список точек патрулирования типа Transform из назначенных в редакторе списка GameObject
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
                  
        public void Init(float enemyhealth,int enemylevel) //инициализация здоровья при спауне призрака
        {
            enemyhealth = _enemyhealth;
            enemylevel = _enemylevel;
            //Destroy(gameObject, 15f); было нужно для автоудаления призрака через заданное время
        }
        private void FixedUpdate()
        {
            Ray ray = new Ray(_spawnPositionBullet.position, transform.forward); //Взгляд призрака на 4 единиц
            
            if (Physics.Raycast(ray, out RaycastHit hit, 4))
            {
                Debug.DrawRay(_spawnPositionBullet.position, transform.forward * hit.distance, Color.blue); //дебаг
                if (hit.collider.CompareTag("Player")) //Стрельба в сторону игрока если расстояние меньше 4 и есть видимость
                {
                    if (_IsFire)
                        Fire();
                }
            }
            //Поворот в сторону игрока если расстояние меньше 5 (имитация слуха)
            if( Vector3.Distance(transform.position, _player.transform.position) < 5)
            {
                transform.LookAt(_player.transform.position); 
            }
            //Движение призрака в направлении _direction
            //Move(Time.fixedDeltaTime);
        }
        private void Fire()
        {
            _IsFire = false;
            var bulletObj = Instantiate(_bulletPrefab, _spawnPositionBullet.position, _spawnPositionBullet.rotation);//спаун пули
            var bullet = bulletObj.GetComponent<Bullet>();//поиск скрипта пули для задания парамеров пули
            bullet.Init(_player.transform,10f,3f); //параметры :пуля летит в сторону игрока со скоростью 3
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
            _enemyhealth -= damage;//вычитаем из здоровья призрака полученный урон
            if (_enemyhealth <=0) //когда здоровье меньше или равно 0
                Destroy(gameObject, 1f); // убраем объект со сцены через сек
        }
        //private IEnumerator Patrol()
        //{
        //    Console.WriteLine("Wait start");
        //    yield return new WaitForSeconds(5f); //пауза перед началом патруля
        //    Console.WriteLine("Start");
        //    foreach (Transform nextTrnsPp in _patrolList)
        //    {
        //        Console.WriteLine("Wait rotate");
        //        yield return new WaitForSeconds(5f); //пауза перед началом поворота
        //        Console.WriteLine("Rotate");
        //        transform.LookAt(nextTrnsPp.transform.position);
        //        Console.WriteLine("Wait move");
        //        yield return new WaitForSeconds(5f); //пауза перед началом движения по маршруту
        //        Console.WriteLine("Move");
        //        _direction = nextTrnsPp.transform.position;
        //        yield return new WaitForSeconds(5f); //пауза перед продолжением движения по маршруту
        //    }
        //}
    }
}

