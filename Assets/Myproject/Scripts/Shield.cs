using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnProject
{
    public class Shield : MonoBehaviour,ITakeDamage
    {
        [SerializeField] private int _durablity = 25; //прочка щита
        public void Init(int durablity)
        {
            durablity = _durablity;
            //Destroy(gameObject, 20f); для пропадания через время
        }

        public void Hit(int damage)
        {
            _durablity -= damage;//вычитаем из прочки щита полученный урон
            if (_durablity <= 0) //когда прочка меньше или равно 0
                Destroy(gameObject, 1f);// убраем щит через сек
        }
    }
}
