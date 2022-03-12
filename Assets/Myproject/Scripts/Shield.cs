using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnProject
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] private float _durablity = 10f;
        public void Init(float durablity)
        {
            durablity = _durablity;
            Destroy(gameObject, 3f);
        }


    }
}
