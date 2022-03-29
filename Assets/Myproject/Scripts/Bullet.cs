using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnProject 
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int _damage = 3; //наносимый урон
        private Transform _target;
        private float _speed = 0.7f; //скорость пули

        public void Init(Transform target, float lifetime, float speed)//префаб,время жизни, скорость
        {
            _target = target;
            _speed = speed;
            Destroy(gameObject, lifetime);
        }

        void FixedUpdate()
        {
            //transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed);
            transform.position += transform.forward * _speed * Time.fixedDeltaTime;
        }
        private void OnCollisionEnter(Collision collision)//если столкнулся
        {
            if (collision.gameObject.TryGetComponent(out ITakeDamage takeDamage)) //если урон может наноситься
            {
                takeDamage.Hit(_damage);//наносим урон
            }
            Destroy(gameObject, 1f); //убираем пулю
        }
    }
}
