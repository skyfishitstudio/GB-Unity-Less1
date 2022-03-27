using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnProject
{
    public class Bomb : MonoBehaviour,ITakeDamage
    {
        [SerializeField] private int _durablityBomb = 1; //прочка бомбы
        [SerializeField] private int _damageBomb = 100; // наносимый бомбой урон
        public void Init(int durablity)
        {
            durablity = _durablityBomb;
           // Destroy(gameObject, 5f);
        }

        public void Hit(int damage)
        {
            _durablityBomb -= damage;
            if (_durablityBomb <= 0)
                Destroy(gameObject);
        }
        private void OnCollisionEnter(Collision collision)
        {   //на все объекты в поле коллизии которые могут получать урон
            if (collision.gameObject.TryGetComponent(out ITakeDamage takeDamage)) 
            {
                takeDamage.Hit(_damageBomb); //наносим урон
            }
        }
    }
}