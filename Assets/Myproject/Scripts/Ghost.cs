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
        [SerializeField] private int _enemylevel;// уровень призрака
        [SerializeField] private float _enemyhealth = 50f; // здоровье призрака
        void Start()
        {
            _player = FindObjectOfType<Player>();
        }

        
        
        public void Init(float enemyhealth,int enemylevel) //инициализация здоровья при спауне призрака
        {
            enemyhealth = _enemyhealth;
            enemylevel = _enemylevel;
            //Destroy(gameObject, 15f); было нужно для автоудаления призрака через заданное время
        }
        private void FixedUpdate()
        {
            //Поворот в сторону игрока если расстояние меньше 4
            if( Vector3.Distance(transform.position, _player.transform.position) < 5)
            {
                transform.LookAt(_player.transform.position); 
            }
            //Стрельба в сторону игрока если расстояние меньше 2
            if (Vector3.Distance(transform.position, _player.transform.position) < 3)
            {
                Fire();
            }
        }
        private void Fire()
        {
            var bulletObj = Instantiate(_bulletPrefab, _spawnPositionBullet.position, _spawnPositionBullet.rotation);//спаун пули
            var bullet = bulletObj.GetComponent<Bullet>();//поиск скрипта пули для задания парамеров пули
            bullet.Init(_player.transform,10f,0.05f); //параметры :пуля летит в сторону игрока со скоростью 0.05f
        }
        public void Hit(int damage)
        {
            _enemyhealth -= damage;//вычитаем из здоровья призрака полученный урон
            if (_enemyhealth <=0) //когда здоровье меньше или равно 0
                Destroy(gameObject, 1f); // убраем объект со сцены через сек
        }
    }
}

