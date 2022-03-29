using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnProject
{
    public class CameraMove : MonoBehaviour
    {
        public float _speedRotate = 0f;

        void FixedUpdate()
        {
            transform.Rotate(new Vector3((Input.GetAxis("Mouse Y") * _speedRotate * Time.deltaTime), 0, 0));
        }
    }
}