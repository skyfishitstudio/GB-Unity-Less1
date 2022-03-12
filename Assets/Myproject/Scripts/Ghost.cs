using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnProject
{
    public class Ghost : MonoBehaviour
    {
        [SerializeField] private Player _player;
        
 
        void Start()
        {
            _player = FindObjectOfType<Player>();
        }

        [SerializeField] private float _durablity = 10f;
        public void Init(float durablity)
        {
            durablity = _durablity;
            Destroy(gameObject, 15f);
        }
    }
}

