using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnProject 
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int _damage = 10; //��������� ����
        [SerializeField] private float _force = 2f; //�������� ���� ��� ������
        [SerializeField] private float _lifetime = 10f;//����� ����� ����
        private Transform _target;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        public void Init(Transform target, float lifetime, float speed)//������,����� �����, ��������
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
        private void OnCollisionEnter(Collision collision)//���� ����������
        {
            if (collision.gameObject.TryGetComponent(out ITakeDamage takeDamage)) //���� ���� ����� ����������
            {
                takeDamage.Hit(_damage);//������� ����
            }
            Destroy(gameObject); //������� ���� �����
        }
    }
}
