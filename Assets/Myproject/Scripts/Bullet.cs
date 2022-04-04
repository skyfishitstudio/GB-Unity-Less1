using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnProject 
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int _damage = 10; //наносимый урон
        [SerializeField] private float _force = 2f; //скорость пули для физики
        [SerializeField] private float _lifetime = 10f;//время жизни пули
        private Transform _target;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        public void Init(Transform target, float lifetime, float speed)//префаб,время жизни, скорость
        {
            _target = target;
          // _speed = speed;
            _lifetime = lifetime;
            Destroy(gameObject, _lifetime);
            _rigidbody.AddForce(transform.forward * _force);        }

        //void FixedUpdate()
        //{
        //   //transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed);
        //   transform.position += transform.forward * _speed * Time.fixedDeltaTime;
        //}
        private void OnCollisionEnter(Collision collision)//если столкнулся
        {
            if (collision.gameObject.TryGetComponent(out ITakeDamage takeDamage)) //если урон может наноситься
            {
                takeDamage.Hit(_damage);//наносим урон
            }
            Destroy(gameObject); //убираем пулю сразу
        }
    }
}
