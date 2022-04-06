using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnProject
{
    public class Trap : MonoBehaviour
    {
        [SerializeField] private float _cooldown = 1f;
        private bool _isHide;

        void Start()
        {
            InvokeRepeating(nameof(Move), 1f,_cooldown);
        }


        public void Move()
        {
            if (_isHide)
            {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }
            else
            {
            transform.position = new Vector3(transform.position.x, -2, transform.position.z);
            }
            _isHide = !_isHide;
        }
        private void OnTriggerStay(Collider other)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CancelInvoke(nameof(Move));
            }
        }
    }
}