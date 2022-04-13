using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

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
        [SerializeField] private bool _patroling = false;
        [SerializeField] private bool _harrasment = false;
        [SerializeField] private Transform[] _waypoints;
        private Transform _pp;
        private Rigidbody _rigidbody;
        private Vector3 _direction;
        private NavMeshAgent _agent;
        int m_CurrentWaypointIndex;
        void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _player = FindObjectOfType<Player>();
        }
       
        void Start()
        {
             StartCoroutine(Patrol( _waypoints));
        }
                  
        public void Init(float enemyhealth,int enemylevel) //инициализация здоровья при спауне призрака
        {
            enemyhealth = _enemyhealth;
            enemylevel = _enemylevel;
            //Destroy(gameObject, 15f); было нужно для автоудаления призрака через заданное время
        }
        private void FixedUpdate()
        {
            if ((_agent.remainingDistance < _agent.stoppingDistance) && _patroling) //если патрулирование активно и дошли до точки переключаем точку на следущую
            {
                
                m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % _waypoints.Length;
                _agent.SetDestination(_waypoints[m_CurrentWaypointIndex].position);
            }
            Ray ray = new Ray(_spawnPositionBullet.position, transform.forward); //Взгляд призрака на 2 единиц

            if (Physics.Raycast(ray, out RaycastHit hit, 2))
            {
                Debug.DrawRay(_spawnPositionBullet.position, transform.forward * hit.distance, Color.blue); //дебаг взгляда призрака
                if (hit.collider.CompareTag("Player")) //Стрельба в сторону игрока если расстояние меньше 2 и есть видимость
                {
                    if (_IsFire)
                        Fire();
                }
            }
            //Поворот в сторону игрока если расстояние меньше 5 (имитация слуха)
            if( Vector3.Distance(transform.position, _player.transform.position) < 5f)
            {
                StopCoroutine(Patrol(_waypoints)); //Если услышали прерываем партуль
                StartCoroutine(Harassment(_player));//Начинаем преследование
            }
            else if (_patroling == false) // если выпал из зоны слуха и патрулирование еще не началось то начнем патруль заново
            {
                StopCoroutine(Harassment(_player)); //Если не в зоне слышимости прерываем преследование
                StartCoroutine(Patrol(_waypoints));//Начинаем партуль
            }
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
        IEnumerator Patrol(Transform[] _waypoints)
        {
            _agent.stoppingDistance = 0;
            _patroling = true;
            _harrasment = false;
            Debug.Log("Wait start");
            yield return new WaitForSeconds(0.5f); //пауза перед началом патруля
            Debug.Log("Start");
            foreach (Transform nextTrnsPp in _waypoints)
            {
                Debug.Log("Wait rotate");
               yield return new WaitForSeconds(1f); //пауза перед началом поворота
                Debug.Log("Rotate");
                transform.LookAt(nextTrnsPp.transform.position);
                Debug.Log("Wait move");
                yield return new WaitForSeconds(1f); //пауза перед началом движения по маршруту
                Debug.Log("Move");
                _agent.SetDestination(nextTrnsPp.transform.position);
                yield return new WaitForSeconds(3f); //пауза перед продолжением движения по маршруту
            }
            StartCoroutine(Patrol(_waypoints));
        }
        IEnumerator Harassment(Player _player)
        {
            _patroling = false;
            _harrasment = true;
            _agent.stoppingDistance = 2;
            yield return new WaitForSeconds(1f); //пауза перед началом поворота
            transform.LookAt(_player.transform.position);
            yield return new WaitForSeconds(1f); //пауза перед началом преследования
            _agent.SetDestination(_player.transform.position);
        }
    }
}

