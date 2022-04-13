using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnProject
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Transform _rotatePoint;
        [SerializeField] private bool _stoppedDoor;
        [SerializeField] private Animator _animDoor;
        private void Awake()
        {
            _animDoor = GetComponent<Animator>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !_stoppedDoor)
                _animDoor.SetBool("IsOpen",true);

        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && !_stoppedDoor)
                _animDoor.SetBool("IsOpen",false);
        }
        private void OnTriggerStay(Collider other)
        {
            if (Input.GetKeyDown(KeyCode.E))
                _animDoor.enabled = false;
                //_stoppedDoor = !_stoppedDoor;
        }
    }
}