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


        private void Move()
        {
            if (_isHide)
            {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }
            else
            {
            transform.position = new Vector3(transform.position.x, -1, transform.position.z);
            }
            _isHide = !_isHide;
        }
    }
}