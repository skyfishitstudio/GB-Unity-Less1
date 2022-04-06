using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnProject
{
    public class Bomb : MonoBehaviour, ITakeDamage
    {
        [SerializeField] private int _durablityBomb = 1; //прочка бомбы
        [SerializeField] private int _damageBomb = 100; // наносимый бомбой урон
        [SerializeField] private int _radiusDamage = 3; // достижимый бомбой радиус
        [SerializeField] private float _force = 0.1f; //сила взрыва бомбы
        public GameObject explosionEffect;
        public void Init(int durablity)
        {
            durablity = _durablityBomb;
        }

        public void Hit(int damage)
        {
            _durablityBomb -= damage;
            if (_durablityBomb <= 0)
            {
                Explode();
                Destroy(gameObject, 1f);
            }     
        }
        public void Explode() //метод реализующий взрыв
        {
            Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radiusDamage);
            for (int i = 0; i < overlappedColliders.Length; i++)
            {
                Rigidbody rigidbody = overlappedColliders[i].attachedRigidbody;
                Vector3 vector3 = overlappedColliders[i].transform.position; // позиция и положение всех объектов попавших в радиус
                if (rigidbody)
                {
                    rigidbody.AddForce((vector3 - transform.position) * _force); // отталкивает в разные стороны при взрыве

                }
            }
            Instantiate(explosionEffect, transform.position, Quaternion.identity);// сичтема частиц взрыва
        }
        private void OnCollisionEnter(Collision collision) // Если наступить на бомбу то бабах
        {   //на все объекты в поле коллизии которые могут получать урон
            if (collision.gameObject.TryGetComponent(out ITakeDamage takeDamage)) 
            {
                takeDamage.Hit(_damageBomb); //наносим урон
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
            Gizmos.DrawWireSphere(transform.position, _radiusDamage);
        }
    }
}