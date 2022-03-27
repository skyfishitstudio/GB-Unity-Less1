using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnProject 
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int _damage = 3; //��������� ����
        private Transform _target;
        private float _speed = 0.7f; //�������� ����

        public void Init(Transform target, float lifetime, float speed)//������,����� �����, ��������
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
        private void OnCollisionEnter(Collision collision)//���� ����������
        {
            if (collision.gameObject.TryGetComponent(out ITakeDamage takeDamage)) //���� ���� ����� ����������
            {
                takeDamage.Hit(_damage);//������� ����
            }
            Destroy(gameObject, 1f); //������� ����
        }
    }
}
