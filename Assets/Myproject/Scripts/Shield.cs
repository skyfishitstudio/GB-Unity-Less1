using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnProject
{
    public class Shield : MonoBehaviour,ITakeDamage
    {
        [SerializeField] private int _durablity = 25; //������ ����
        public void Init(int durablity)
        {
            durablity = _durablity;
            //Destroy(gameObject, 20f); ��� ���������� ����� �����
        }

        public void Hit(int damage)
        {
            _durablity -= damage;//�������� �� ������ ���� ���������� ����
            if (_durablity <= 0) //����� ������ ������ ��� ����� 0
                Destroy(gameObject, 1f);// ������ ��� ����� ���
        }
    }
}
